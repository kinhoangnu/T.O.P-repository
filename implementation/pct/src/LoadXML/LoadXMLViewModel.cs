using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using com.vanderlande.wpf;
using Com.Vanderlande.Top.Common.Fmc.BusinessService;
using Com.Vanderlande.Top.Common.Fmc.BusinessService.Exception;
using Com.Vanderlande.Top.Configuration.Fmc.BusinessService;
using Com.Vanderlande.Top.Configuration.Fmc.BusinessService.Event;
using Com.Vanderlande.Top.Configuration.Fmc.Model;
using Com.Vanderlande.Top.Configuration.Fmc.Model.DataAccess;
using Com.Vanderlande.Top.HumanResource.Fmc.BusinessService;
using Com.Vanderlande.Top.Resource.Fmc.BusinessService;
using Com.Vanderlande.Top.Resource.Fmc.Model.Entities;
using Configurations;
using FM.Top.TopIntModel;
using FM.Top.TopIntTypes;
using Parameters = Com.Vanderlande.Top.Common.Fmc.BusinessService.Parameters;

namespace Your
{
    public class LoadXmlViewModel : ContentViewModel
    {
        private readonly XmlReaderSettings setting = new XmlReaderSettings();
        private ObservableCollection<Buffer> tempBlist = new ObservableCollection<Buffer>();
        private XmlDocument doc = new XmlDocument();
        private ConfigurationService con = new ConfigurationService();
        private string path = string.Empty;
        private string xmlInputData = string.Empty;
        private string xmlOutputData = string.Empty;
        private List<string> stringlist;
        private ObservableCollection<SecondaryActivity> tempSClist;
        private TopProjectModel topConfigurationObject;
        public RelayCommand LoadCommand { get; set; }
        public RelayCommand SaveCommand { get; set; }

        public LoadXmlViewModel()
        {
            LoadCommand = new RelayCommand(obj => Import());
            SaveCommand = new RelayCommand(obj => Export());
        }

        /// <summary>
        /// Load the XML file
        /// </summary>
        public void Import()
        {
            Exception exception = null;
            ValidationEventHandler validationHandler = (sender, args) =>
            {
                if (args.Severity != XmlSeverityType.Error)
                {
                    return;
                }
                if (exception == null)
                {
                    exception = args.Exception;
                }
            };
            setting.ValidationEventHandler += validationHandler;
            var openfiledialog = new OpenFileDialog();
            if (openfiledialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            var loadpath = openfiledialog.FileName;
            var serializer = new XmlSerializer(typeof(TopProjectModel));
            using (var reader = XmlReader.Create(loadpath))
            {
                topConfigurationObject = (TopProjectModel) serializer.Deserialize(reader);
                Load(topConfigurationObject);
            }
            ImportProdArea();
            ImportBuffer();
            ImportProcess();
            ImportSecondaryActivity();
            ImportWorkstationClass();
            ImportWorkstationGroup();
            ImportWorkstation();
            ImportOperator();
        }

        public void Export()
        {
            var serializer = new XmlSerializer(typeof(TopProjectModel));
            var i = -1;
            ExportBuffer(i);
            ExportProdArea(i);
            ExportWorkstationGroup(i);
            ExportSecondaryActivity(i);

            var xmlDocument = new XmlDocument();
            var saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            var savepath = saveFileDialog.FileName;
            using (var stream = new MemoryStream())
            {
                Load(topConfigurationObject);
                serializer.Serialize(stream, topConfigurationObject);
                stream.Position = 0;
                xmlDocument.Load(stream);
                xmlDocument.Save(savepath);
                stream.Close();
            }
        }

        public string Productiontype(ProdArea p)
        {
            return p.PType;
        }

        public void Load(TopProjectModel topProjectModel)
        {
            var database = new MemoryDb();
            var inMemoryParameters = new Parameters(new ParameterMemoryDao(database));
            var inMemoryResourceDefinitions = new ResourceDefinitions(new AreaMemoryDao(database),
                new WorkstationMemoryDao(database),
                new ProcessMemoryDao(database), new BufferMemoryDao(database), new WorkstationClassMemoryDao(database),
                new ResourceDefinitionMemoryDao<WorkstationGroupEntity>(database), new ActivityMemoryDao(database),
                new PlannableActivityMemoryDao(database));
            var inMemoryHumanResources = new HumanResources(inMemoryResourceDefinitions, null, null,
                new OperatorDefinitionMemoryDao(database), new OperatorAlgorithmMemoryDao(database), inMemoryParameters);
            var inMemoryExtensibleEnumerations = new ExtensibleEnumerations(new ExtensibleEnumerationMemoryDao(database));
            var loader = new TopProjectConfigurationLoader(inMemoryResourceDefinitions, inMemoryHumanResources,
                inMemoryParameters, inMemoryExtensibleEnumerations);
            IParameterEditor inMemoryParameterEditor = new ParameterEditor(inMemoryParameters,
                new ConfigurationElementMemoryDao(database));

            loader.Load(topProjectModel);

            var validationResult = inMemoryResourceDefinitions.Validate();
            validationResult = validationResult.Concat(inMemoryHumanResources.Validate());
            validationResult = validationResult.Concat(inMemoryParameterEditor.Validate());

            if (validationResult.HasFailures())
            {
                throw new ValidationException(validationResult.Results.SelectMany(result => result.Errors).ToArray());
            }
            ConfigurationEvents.Instance.RaiseStartedLoadingConfiguration();
        }

        public void ImportProdArea()
        {
            foreach (var p in topConfigurationObject.ProductionAreas)
            {
                ProdAreaList.ProdAreas.Add(new ProdArea
                {
                    PName = p.ObjectIdentification.Name,
                    PComId = p.CommunicationId,
                    PDescription = p.ObjectIdentification.Description,
                    PType = p.ProductionType.ToString(),
                    Uuid = p.ObjectIdentification.UUID
                });
            }
        }

        public void ExportProdArea(int i)
        {
            topConfigurationObject.ProductionAreas = new ProductionArea[ProdAreaList.ProdAreas.Count];
            foreach (var prod in ProdAreaList.ProdAreas)
            {
                if (prod.PType == "Inbound")
                {
                    i += 1;
                    topConfigurationObject.ProductionAreas[i] =
                        new ProductionArea
                        {
                            CommunicationId = prod.PComId,
                            ProductionType = ProductionType.Inbound,
                            ObjectIdentification = new ObjectIdentification
                            {
                                UUID = prod.Uuid,
                                Description = prod.PDescription,
                                Name = prod.PName
                            }
                        };
                }
                else if (prod.PType == "Outbound")
                {
                    i += 1;
                    topConfigurationObject.ProductionAreas[i] =
                        new ProductionArea
                        {
                            CommunicationId = prod.PComId,
                            ProductionType = ProductionType.Outbound,
                            ObjectIdentification = new ObjectIdentification
                            {
                                UUID = prod.Uuid,
                                Description = prod.PDescription,
                                Name = prod.PName
                            }
                        };
                }
            }
        }

        public void ImportBuffer()
        {
            foreach (var b in topConfigurationObject.Buffers)
            {
                BufferList.Buffers.Add(new Buffer
                {
                    BComId = b.CommunicationId,
                    BDescription = b.ObjectIdentification.Description,
                    BName = b.ObjectIdentification.Name,
                    BUnit = b.Unit,
                    Uuid = b.ObjectIdentification.UUID
                });
            }
        }

        public void ExportBuffer(int i)
        {
            topConfigurationObject.Buffers = new FM.Top.TopIntTypes.Buffer[BufferList.Buffers.Count];
            foreach (var b in BufferList.Buffers)
            {
                i += 1;
                topConfigurationObject.Buffers[i] =
                    new FM.Top.TopIntTypes.Buffer
                    {
                        CommunicationId = b.BComId,
                        Unit = b.BUnit,
                        ObjectIdentification = new ObjectIdentification
                        {
                            UUID = b.Uuid,
                            Description = b.BDescription,
                            Name = b.BName
                        }
                    };
            }
        }

        public void ImportProcess()
        {
            foreach (var pc in topConfigurationObject.Processes)
            {
                if (pc.OutBuffers != null && pc.OutBuffers.Any())
                {
                    tempBlist = new ObservableCollection<Buffer>();

                    for (var i = 0; i < pc.OutBuffers.Count(); i++)
                    {
                        tempBlist.Add(BufferList.GetABuffer(pc.OutBuffers[i]));
                    }
                    foreach (var b in BufferList.Buffers)
                    {
                        if (tempBlist.Contains(b))
                        {
                            continue;
                        }
                        foreach (var b1 in tempBlist)
                        {
                            if (b.BName == b1.BName)
                            {
                                goto Outer;
                            }
                        }
                        //tempBlist.Add(b);
                        tempBlist.Add(BufferList.GetANotSelectedBuffer(b.Uuid));
                        Outer:
                        ;
                    }
                    ProcessList.Processes.Add(new Process
                    {
                        PcComId = pc.CommunicationId,
                        PcDescription = pc.ObjectIdentification.Description,
                        PcName = pc.ObjectIdentification.Name,
                        Uuid = pc.ObjectIdentification.UUID,
                        ExclFromKpi = pc.ExcludeFromKPIs,
                        IsReplenished = pc.IsReplenishment,
                        ProdRef = ProdAreaList.GetAProdArea(pc.ProductionAreaRef),
                        InbufferRef = BufferList.GetABuffer(pc.InBuffer),
                        ObservableOutBuffer = tempBlist
                    });
                }
                else
                {
                    ProcessList.Processes.Add(new Process
                    {
                        PcComId = pc.CommunicationId,
                        PcDescription = pc.ObjectIdentification.Description,
                        PcName = pc.ObjectIdentification.Name,
                        Uuid = pc.ObjectIdentification.UUID,
                        ExclFromKpi = pc.ExcludeFromKPIs,
                        IsReplenished = pc.IsReplenishment,
                        ProdRef = ProdAreaList.GetAProdArea(pc.ProductionAreaRef),
                        InbufferRef = BufferList.GetABuffer(pc.InBuffer),
                        ObservableOutBuffer = BufferList.Buffers
                    });
                }
            }
        }

        public void ImportSecondaryActivity()
        {
            foreach (var s in topConfigurationObject.SecondaryActivities)
            {
                SecondaryActivityList.SecondaryActivities.Add(new SecondaryActivity
                {
                    ScComId = s.CommunicationId,
                    ScDescription = s.ObjectIdentification.Description,
                    ScName = s.ObjectIdentification.Name,
                    Uuid = s.ObjectIdentification.UUID
                });
            }
        }

        public void ExportSecondaryActivity(int i)
        {
            topConfigurationObject.SecondaryActivities =
                new FM.Top.TopIntTypes.SecondaryActivity[SecondaryActivityList.SecondaryActivities.Count];
            foreach (var sc in SecondaryActivityList.SecondaryActivities)
            {
                i += 1;
                topConfigurationObject.SecondaryActivities[i] =
                    new FM.Top.TopIntTypes.SecondaryActivity
                    {
                        CommunicationId = sc.ScComId,
                        ObjectIdentification = new ObjectIdentification
                        {
                            UUID = sc.Uuid,
                            Description = sc.ScDescription,
                            Name = sc.ScName
                        }
                    };
            }
        }

        public void ImportWorkstationGroup()
        {
            foreach (var wg in topConfigurationObject.WorkstationGroups)
            {
                WorkstationGroupList.WorkstationGroups.Add(new WorkstationGroup
                {
                    WgDescription = wg.ObjectIdentification.Description,
                    WgName = wg.ObjectIdentification.Name,
                    Uuid = wg.ObjectIdentification.UUID
                });
            }
        }

        public void ExportWorkstationGroup(int i)
        {
            topConfigurationObject.WorkstationGroups =
                new FM.Top.TopIntTypes.WorkstationGroup[WorkstationGroupList.WorkstationGroups.Count];
            foreach (var wg in WorkstationGroupList.WorkstationGroups)
            {
                i += 1;
                topConfigurationObject.WorkstationGroups[i] =
                    new FM.Top.TopIntTypes.WorkstationGroup
                    {
                        ObjectIdentification = new ObjectIdentification
                        {
                            UUID = wg.Uuid,
                            Description = wg.WgDescription,
                            Name = wg.WgName
                        }
                    };
            }
        }

        public void ImportWorkstationClass()
        {
            foreach (var wc in topConfigurationObject.WorkstationClasses)
            {
                if (wc.SecondaryActivities != null && wc.SecondaryActivities.Any())
                {
                    tempSClist = new ObservableCollection<SecondaryActivity>();

                    for (var i = 0; i < wc.SecondaryActivities.Count(); i++)
                    {
                        tempSClist.Add(
                            SecondaryActivityList.GetASecondaryActivity(wc.SecondaryActivities[i].ObjectRef));
                    }
                    foreach (var s in SecondaryActivityList.SecondaryActivities)
                    {
                        if (tempSClist.Contains(s))
                        {
                            continue;
                        }
                        foreach (var s1 in tempSClist)
                        {
                            if (s.ScName == s1.ScName)
                            {
                                goto Outer;
                            }
                        }
                        //tempSClist.Add(s);
                        tempSClist.Add(SecondaryActivityList.GetANotSelectedSecondaryActivity(s.Uuid));
                        Outer:
                        ;
                    }
                    WorkstationClassList.WorkstationClasses.Add(new WorkstationClass
                    {
                        WcHandlingType = wc.HandlingType.ToString(),
                        WcName = wc.ObjectIdentification.Name,
                        WcType = wc.WorkstationType,
                        Uuid = wc.ObjectIdentification.UUID,
                        ProcessRef = ProcessList.GetAProcess(wc.ProcessRef),
                        SecondaryactivityRef = tempSClist
                    });
                }
                else
                {
                    WorkstationClassList.WorkstationClasses.Add(new WorkstationClass
                    {
                        WcHandlingType = wc.HandlingType.ToString(),
                        WcName = wc.ObjectIdentification.Name,
                        WcType = wc.WorkstationType,
                        Uuid = wc.ObjectIdentification.UUID,
                        ProcessRef = ProcessList.GetAProcess(wc.ProcessRef),
                        SecondaryactivityRef = SecondaryActivityList.SecondaryActivities
                    });
                }
            }
        }

        public void ImportWorkstation()
        {
            foreach (var w in topConfigurationObject.Workstations)
            {
                if (w.WorkstationGroupRef != null)
                {
                    WorkstationList.Workstations.Add(new Workstation
                    {
                        WComId = w.CommunicationId,
                        WDescription = w.ObjectIdentification.Description,
                        WName = w.ObjectIdentification.Name,
                        Uuid = w.ObjectIdentification.UUID,
                        WorkstationclassRef = WorkstationClassList.GetAWorkstationClass(w.WorkstationClassRef),
                        WorkstationgroupRef = WorkstationGroupList.GetAWorkstationGroup(w.WorkstationGroupRef)
                    });
                }
                else
                {
                    WorkstationList.Workstations.Add(new Workstation
                    {
                        WComId = w.CommunicationId,
                        WDescription = w.ObjectIdentification.Description,
                        WName = w.ObjectIdentification.Name,
                        Uuid = w.ObjectIdentification.UUID,
                        WorkstationclassRef = WorkstationClassList.GetAWorkstationClass(w.WorkstationClassRef)
                    });
                }
            }
        }

        public void ImportOperator()
        {
            foreach (var o in topConfigurationObject.Operators)
            {
                OperatorList.Operators.Add(new Operator
                {
                    ODescription = o.ObjectIdentification.Description,
                    OName = o.ObjectIdentification.Name,
                    OUseCustomUpperLimit = o.UseCustomUpperLimit,
                    Uuid = o.ObjectIdentification.UUID
                });
            }
        }
    }
}