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
using FM.Top.TopIntModel;
using FM.Top.TopIntTypes;
using Your.LoadXML;
using Parameters = Com.Vanderlande.Top.Common.Fmc.BusinessService.Parameters;

namespace Your
{
    public class LoadXmlViewModel : ContentViewModel
    {
        //Fields
        private readonly XmlReaderSettings setting = new XmlReaderSettings();
        private ObservableCollection<Buffer> tempBlist = new ObservableCollection<Buffer>();
        private ObservableCollection<SecondaryActivity> tempSClist;
        private TopProjectModel topConfigurationObject;
        private xmlFile xml;

        //Commands
        public RelayCommand ExportCommand { get; set; }
        public RelayCommand ImportCommand { get; set; }
        public RelayCommand DirrectImportCommand { get; set; }
        public RelayCommand DirrectExportCommand { get; set; }

        public FileInfo[] Files { get; set; }

        public List<xmlFile> FileStrings { get; set; }

        /// <summary>
        /// File that is being selected on the list
        /// </summary>
        public xmlFile SelectedXmlFile
        {
            get { return xml; }
            set { ChangeProperty(ref xml, value); }
        }

        public LoadXmlViewModel()
        {
            LoadFiles();
            ExportCommand = new RelayCommand(obj => Export());
            ImportCommand = new RelayCommand(obj => Import());
            DirrectExportCommand = new RelayCommand(obj => DirrectExport());
            DirrectImportCommand = new RelayCommand(obj => DirrectImport());
        }

        public void DirrectExport()
        {
        }

        public void DirrectImport()
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
            //var openfiledialog = new OpenFileDialog();
            //if (openfiledialog.ShowDialog() != DialogResult.OK)
            //{
            //    return;
            //}
            var loadpath = SelectedXmlFile.FileDirectory;
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

        /// <summary>
        /// Load available XML files from project location
        /// </summary>
        public void LoadFiles()
        {
            FileStrings = new List<xmlFile>();
            var di = new DirectoryInfo(@"XML_Files");
            Files = di.GetFiles();
            foreach (var f in Files)
            {
                FileStrings.Add(new xmlFile
                {
                    Filename = f.Name,
                    Filenumber = 1,
                    FileDirectory = f.DirectoryName
                });
            }
        }

        /// <summary>
        /// Import the XML file to the UI
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

        /// <summary>
        /// Export data to XML file
        /// </summary>
        public void Export()
        {
            var serializer = new XmlSerializer(typeof(TopProjectModel));
            var i = -1;
            ExportBuffer(i);
            ExportProdArea(i);
            ExportWorkstationGroup(i);
            ExportSecondaryActivity(i);
            ExportProcess(i);
            ExportWorkstation(i);
            ExportWorkstationClass(i);

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

        /// <summary>
        /// Validate the given "top project model"
        /// </summary>
        /// <param name="topProjectModel"></param>
        public void Load(TopProjectModel topProjectModel)
        {
            //try
            //{
            var database = new MemoryDb();
            var inMemoryParameters = new Parameters(new ParameterMemoryDao(database));
            var inMemoryResourceDefinitions = new ResourceDefinitions(new AreaMemoryDao(database),
                new WorkstationMemoryDao(database),
                new ProcessMemoryDao(database), new BufferMemoryDao(database),
                new WorkstationClassMemoryDao(database),
                new ResourceDefinitionMemoryDao<WorkstationGroupEntity>(database), new ActivityMemoryDao(database),
                new PlannableActivityMemoryDao(database));
            var inMemoryHumanResources = new HumanResources(inMemoryResourceDefinitions, null, null,
                new OperatorDefinitionMemoryDao(database), new OperatorAlgorithmMemoryDao(database),
                inMemoryParameters);
            var inMemoryExtensibleEnumerations =
                new ExtensibleEnumerations(new ExtensibleEnumerationMemoryDao(database));
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
                Exception e =
                    new ValidationException(validationResult.Results.SelectMany(result => result.Errors).ToArray());
                MessageBox.Show("There was an error. Details: " + e.Message);
            }

            //}
            //catch (Exception e)
            //{
            //    MessageBox.Show("There was an error. Details: " + e.Message);
            //}
            ConfigurationEvents.Instance.RaiseStartedLoadingConfiguration();
        }

        public void ImportProdArea()
        {
            foreach (var p in topConfigurationObject.ProductionAreas)
            {
                switch (p.ProductionType)
                {
                    case ProductionType.Inbound:
                        ProdAreaList.ProdAreas.Add(new ProdArea
                        {
                            PName = p.ObjectIdentification.Name,
                            PComId = p.CommunicationId,
                            PDescription = p.ObjectIdentification.Description,
                            PType = "Inbound",
                            Uuid = p.ObjectIdentification.UUID
                        });
                        break;
                    case ProductionType.Outbound:
                        ProdAreaList.ProdAreas.Add(new ProdArea
                        {
                            PName = p.ObjectIdentification.Name,
                            PComId = p.CommunicationId,
                            PDescription = p.ObjectIdentification.Description,
                            PType = "Outbound",
                            Uuid = p.ObjectIdentification.UUID
                        });
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
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

        public void ExportProcess(int i)
        {
            var count = -1;
            topConfigurationObject.Processes = new FM.Top.TopIntTypes.Process[ProcessList.Processes.Count];
            foreach (var p in ProcessList.Processes)
            {
                var tempOutBufferStrings =
                    (from buffer in p.ObservableOutBuffer where buffer.IsSelected select buffer.Uuid).ToList();
                var tempOutBufferArray = new string[tempOutBufferStrings.Count];
                foreach (var s in tempOutBufferStrings)
                {
                    count += 1;
                    tempOutBufferArray[count] = s;
                }
                count = -1;
                i += 1;
                topConfigurationObject.Processes[i] =
                    new FM.Top.TopIntTypes.Process
                    {
                        CommunicationId = p.PcComId,
                        IsReplenishment = p.IsReplenished,
                        ExcludeFromKPIs = p.ExclFromKpi,
                        InBuffer = p.InbufferRef.Uuid,
                        OutBuffers = tempOutBufferArray,
                        ProductionAreaRef = p.ProdRef.Uuid,
                        ObjectIdentification = new ObjectIdentification
                        {
                            UUID = p.Uuid,
                            Description = p.PcDescription,
                            Name = p.PcName
                        }
                    };
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
                        if (wc.SecondaryActivities[i].MaxAllowedSpecified)
                        {
                            tempSClist.Add(
                                SecondaryActivityList.GetASecondaryActivitywithMaxAllow(
                                    wc.SecondaryActivities[i].ObjectRef,
                                    Convert.ToInt16(wc.SecondaryActivities[i].MaxAllowed)));
                        }
                        else if (!wc.SecondaryActivities[i].MaxAllowedSpecified)
                        {
                            tempSClist.Add(
                                SecondaryActivityList.GetASecondaryActivitywithoutMaxAllow(
                                    wc.SecondaryActivities[i].ObjectRef));
                        }
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

        public void ExportWorkstationClass(int i)
        {
            topConfigurationObject.WorkstationClasses =
                new FM.Top.TopIntTypes.WorkstationClass[WorkstationClassList.WorkstationClasses.Count];
            foreach (var wc in WorkstationClassList.WorkstationClasses)
            {
                var count = -1;
                var countForMax = -1;
                var tempSecondaryActivityStrings = new List<string>();
                var tempMaxAllow = new List<long>();
                foreach (var secondaryactivity in wc.SecondaryactivityRef)
                {
                    if (!secondaryactivity.IsSelected)
                    {
                        continue;
                    }
                    tempSecondaryActivityStrings.Add(secondaryactivity.Uuid);
                    if (!secondaryactivity.MaxAllowedSpecified)
                    {
                        continue;
                    }
                    tempMaxAllow.Add(secondaryactivity.MaxAllowed);
                    countForMax += 1;
                }

                #region Has Max Allow

                if (countForMax >= 0)
                {
                    var tempSecondaryActivityArray = new WCSecondaryActivity[tempSecondaryActivityStrings.Count];
                    foreach (var s in tempSecondaryActivityStrings)
                    {
                        count += 1;
                        if (tempMaxAllow.Count > countForMax)
                        {
                            countForMax += 1;
                            tempSecondaryActivityArray[count] = new WCSecondaryActivity
                            {
                                MaxAllowedSpecified = true,
                                MaxAllowed = tempMaxAllow.ElementAt(tempSecondaryActivityStrings.IndexOf(s)),
                                ObjectRef = s
                            };
                        }
                        else
                        {
                            tempSecondaryActivityArray[count] = new WCSecondaryActivity
                            {
                                ObjectRef = s
                            };
                        }
                    }
                    i += 1;
                    if (wc.WcHandlingType == "Automatic")
                    {
                        topConfigurationObject.WorkstationClasses[i] =
                            new FM.Top.TopIntTypes.WorkstationClass
                            {
                                ProcessRef = wc.ProcessRef.Uuid,
                                SecondaryActivities = tempSecondaryActivityArray,
                                WorkstationType = wc.WcType,
                                HandlingType = HandlingType.Automatic,
                                ObjectIdentification = new ObjectIdentification
                                {
                                    UUID = wc.Uuid,
                                    Name = wc.WcName
                                }
                            };
                    }
                    else if (wc.WcHandlingType == "Manual")
                    {
                        topConfigurationObject.WorkstationClasses[i] =
                            new FM.Top.TopIntTypes.WorkstationClass
                            {
                                ProcessRef = wc.ProcessRef.Uuid,
                                SecondaryActivities = tempSecondaryActivityArray,
                                WorkstationType = wc.WcType,
                                HandlingType = HandlingType.Manual,
                                ObjectIdentification = new ObjectIdentification
                                {
                                    UUID = wc.Uuid,
                                    Name = wc.WcName
                                }
                            };
                    }
                    else if (wc.WcHandlingType == "SemiAutomatic")
                    {
                        topConfigurationObject.WorkstationClasses[i] =
                            new FM.Top.TopIntTypes.WorkstationClass
                            {
                                ProcessRef = wc.ProcessRef.Uuid,
                                SecondaryActivities = tempSecondaryActivityArray,
                                WorkstationType = wc.WcType,
                                HandlingType = HandlingType.SemiAutomatic,
                                ObjectIdentification = new ObjectIdentification
                                {
                                    UUID = wc.Uuid,
                                    Name = wc.WcName
                                }
                            };
                    }
                }
                    #endregion

                    #region Doesn't have Max Allow

                else
                {
                    if (tempSecondaryActivityStrings.Count > 0)
                    {
                        var tempSecondaryActivityArray = new WCSecondaryActivity[tempSecondaryActivityStrings.Count];
                        foreach (var s in tempSecondaryActivityStrings)
                        {
                            count += 1;
                            tempSecondaryActivityArray[count] = new WCSecondaryActivity
                            {
                                ObjectRef = s
                            };
                        }
                        i += 1;
                        if (wc.WcHandlingType == "Automatic")
                        {
                            topConfigurationObject.WorkstationClasses[i] =
                                new FM.Top.TopIntTypes.WorkstationClass
                                {
                                    ProcessRef = wc.ProcessRef.Uuid,
                                    SecondaryActivities = tempSecondaryActivityArray,
                                    WorkstationType = wc.WcType,
                                    HandlingType = HandlingType.Automatic,
                                    ObjectIdentification = new ObjectIdentification
                                    {
                                        UUID = wc.Uuid,
                                        Name = wc.WcName
                                    }
                                };
                        }
                        else if (wc.WcHandlingType == "Manual")
                        {
                            topConfigurationObject.WorkstationClasses[i] =
                                new FM.Top.TopIntTypes.WorkstationClass
                                {
                                    ProcessRef = wc.ProcessRef.Uuid,
                                    SecondaryActivities = tempSecondaryActivityArray,
                                    WorkstationType = wc.WcType,
                                    HandlingType = HandlingType.Manual,
                                    ObjectIdentification = new ObjectIdentification
                                    {
                                        UUID = wc.Uuid,
                                        Name = wc.WcName
                                    }
                                };
                        }
                        else if (wc.WcHandlingType == "SemiAutomatic")
                        {
                            topConfigurationObject.WorkstationClasses[i] =
                                new FM.Top.TopIntTypes.WorkstationClass
                                {
                                    ProcessRef = wc.ProcessRef.Uuid,
                                    SecondaryActivities = tempSecondaryActivityArray,
                                    WorkstationType = wc.WcType,
                                    HandlingType = HandlingType.SemiAutomatic,
                                    ObjectIdentification = new ObjectIdentification
                                    {
                                        UUID = wc.Uuid,
                                        Name = wc.WcName
                                    }
                                };
                        }
                    }
                    else
                    {
                        i += 1;
                        if (wc.WcHandlingType == "Automatic")
                        {
                            topConfigurationObject.WorkstationClasses[i] =
                                new FM.Top.TopIntTypes.WorkstationClass
                                {
                                    ProcessRef = wc.ProcessRef.Uuid,
                                    WorkstationType = wc.WcType,
                                    HandlingType = HandlingType.Automatic,
                                    ObjectIdentification = new ObjectIdentification
                                    {
                                        UUID = wc.Uuid,
                                        Name = wc.WcName
                                    }
                                };
                        }
                        else if (wc.WcHandlingType == "Manual")
                        {
                            topConfigurationObject.WorkstationClasses[i] =
                                new FM.Top.TopIntTypes.WorkstationClass
                                {
                                    ProcessRef = wc.ProcessRef.Uuid,
                                    WorkstationType = wc.WcType,
                                    HandlingType = HandlingType.Manual,
                                    ObjectIdentification = new ObjectIdentification
                                    {
                                        UUID = wc.Uuid,
                                        Name = wc.WcName
                                    }
                                };
                        }
                        else if (wc.WcHandlingType == "SemiAutomatic")
                        {
                            topConfigurationObject.WorkstationClasses[i] =
                                new FM.Top.TopIntTypes.WorkstationClass
                                {
                                    ProcessRef = wc.ProcessRef.Uuid,
                                    WorkstationType = wc.WcType,
                                    HandlingType = HandlingType.SemiAutomatic,
                                    ObjectIdentification = new ObjectIdentification
                                    {
                                        UUID = wc.Uuid,
                                        Name = wc.WcName
                                    }
                                };
                        }
                    }
                }

                #endregion
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

        public void ExportWorkstation(int i)
        {
            topConfigurationObject.Workstations = new FM.Top.TopIntTypes.Workstation[WorkstationList.Workstations.Count];
            foreach (var workstation in WorkstationList.Workstations)
            {
                i += 1;
                if (workstation.WorkstationgroupRef != null)
                {
                    topConfigurationObject.Workstations[i] = new FM.Top.TopIntTypes.Workstation
                    {
                        ObjectIdentification = new ObjectIdentification
                        {
                            UUID = workstation.Uuid,
                            Name = workstation.WName,
                            Description = workstation.WDescription
                        },
                        CommunicationId = workstation.WComId,
                        WorkstationClassRef = workstation.WorkstationclassRef.Uuid,
                        WorkstationGroupRef = workstation.WorkstationgroupRef.Uuid
                    };
                }
                else if (workstation.WorkstationgroupRef == null)
                {
                    topConfigurationObject.Workstations[i] = new FM.Top.TopIntTypes.Workstation
                    {
                        ObjectIdentification = new ObjectIdentification
                        {
                            UUID = workstation.Uuid,
                            Name = workstation.WName,
                            Description = workstation.WDescription
                        },
                        CommunicationId = workstation.WComId,
                        WorkstationClassRef = workstation.WorkstationclassRef.Uuid
                    };
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

        private List<string> DirSearch(string sDir)
        {
            var files = new List<string>();
            try
            {
                files.AddRange(Directory.GetFiles(sDir));
                foreach (var d in Directory.GetDirectories(sDir))
                {
                    files.AddRange(DirSearch(d));
                }
            }
            catch (Exception excpt)
            {
                MessageBox.Show(excpt.Message);
            }

            return files;
        }
    }
}