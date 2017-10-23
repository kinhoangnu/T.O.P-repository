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
        public RelayCommand LoadCommand { get; set; }
        public RelayCommand SaveCommand { get; set; }

        public LoadXmlViewModel()
        {
            LoadCommand = new RelayCommand(obj => Load());
            SaveCommand = new RelayCommand(obj => Save());
        }

        /// <summary>
        /// Load the XML file
        /// </summary>
        public void Load()
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
            TopProjectModel topConfigurationObject;
            using (var reader = XmlReader.Create(loadpath))
            {
                topConfigurationObject = (TopProjectModel) serializer.Deserialize(reader);
                Load(topConfigurationObject);
            }

            #region Import Production Area

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

            #endregion

            #region Import Buffer

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

            #endregion

            #region Import Process

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

            #endregion

            #region Import Secondary Activity

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

            #endregion

            #region Import Workstation group

            foreach (var wg in topConfigurationObject.WorkstationGroups)
            {
                WorkstationGroupList.WorkstationGroups.Add(new WorkstationGroup
                {
                    WgDescription = wg.ObjectIdentification.Description,
                    WgName = wg.ObjectIdentification.Name,
                    Uuid = wg.ObjectIdentification.UUID
                });
            }

            #endregion

            #region Import Workstation class

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

            #endregion

            #region Import Workstation

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

            #endregion

            #region Import Operator

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

            #endregion
        }

        public void Save()
        {
            var serializer = new XmlSerializer(typeof(TopProjectModel));
            TopProjectModel topConfigurationObject;
            var xmlDocument = new XmlDocument();
            var saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            var savepath = saveFileDialog.FileName;
            using (var stream = new MemoryStream())
            {
                serializer.Serialize(stream, BufferList.Buffers);
                stream.Position = 0;
                xmlDocument.Load(stream);
                xmlDocument.Save(savepath + ".xml");
                stream.Close();
            }
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
    }
}