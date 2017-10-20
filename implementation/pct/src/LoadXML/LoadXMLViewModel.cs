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
using Configurations;
using FM.Top.TopIntModel;

namespace Your
{
    public class LoadXmlViewModel : ContentViewModel
    {
        private readonly XmlReaderSettings setting = new XmlReaderSettings();
        private XmlDocument doc = new XmlDocument();
        private ConfigurationService con = new ConfigurationService();
        private string path = string.Empty;
        private string xmlInputData = string.Empty;
        private string xmlOutputData = string.Empty;
        private List<string> stringlist;
        private ObservableCollection<SecondaryActivity> tempSClist;
        private ObservableCollection<Buffer> tempBlist;
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
            try
            {
                var openfiledialog = new OpenFileDialog();
                if (openfiledialog.ShowDialog() == DialogResult.OK)
                {
                    var path = openfiledialog.FileName;
                    var serializer = new XmlSerializer(typeof(TopProjectModel));
                    TopProjectModel topConfigurationObject;
                    using (var reader = XmlReader.Create(path))
                    {
                        topConfigurationObject = (TopProjectModel) serializer.Deserialize(reader);
                    }
                    if (topConfigurationObject == null)
                    {
                        //throw new ValidationException();
                        throw new ArgumentException("something went wrong", "original");
                    }

                    if (exception != null)
                    {
                        //throw new ValidationException();
                        throw new ArgumentException("something went wrong", "original");
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
                        if (pc.OutBuffers != null && pc.OutBuffers.Count() > 0)
                        {
                            tempBlist = new ObservableCollection<Buffer>();

                            for (var i = 0; i < pc.OutBuffers.Count(); i++)
                            {
                                tempBlist.Add(BufferList.GetABuffer(pc.OutBuffers[i]));
                            }
                            foreach (var b in BufferList.Buffers)
                            {
                                if (!tempBlist.Contains(b))
                                {
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
                                if (!tempSClist.Contains(s))
                                {
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

                    //ConfigurationService.DeserializeAndValidate<TopProjectModel>(reader, null);
                    //XmlSerializer ser = new XmlSerializer(typeof(Buffers));
                    //reader.ReadToDescendant("Buffers");
                    //object input = ser.Deserialize(reader);
                    //Buffers XmlData = (Buffers)input;
                    //reader.Close();  
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error details: " + e.Message);
            }
        }

        public void Save()
        {
            Exception exception = null;
            //try
            //{
            //    var topConfigurationObject = new TopProjectModel();

            //    SaveFileDialog saveFileDialog = new SaveFileDialog();
            //    if(saveFileDialog.ShowDialog() == DialogResult.OK)
            //    {
            //        var path = saveFileDialog.FileName;
            //        var serializer = new XmlSerializer(typeof(TopProjectModel));

            //        serializer.Serialize(path, b);
            //    }
            //}
            try
            {
                var xmlDocument = new XmlDocument();
                var serializer = new XmlSerializer(typeof(ObservableCollection<FM.Top.TopIntTypes.Buffer>));
                var saveFileDialog = new SaveFileDialog();
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
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
            }
            catch (Exception e)
            {
                MessageBox.Show("Error details: " + e.Message);
            }
        }

        //    CreateParameters(topProjectModel.Parameters);
        //{

        //public void Load(TopProjectModel topProjectModel)
        //}
        //    }
        //        throw new ValidationException(validationResult.Results.SelectMany(result => result.Errors).ToArray());
        //    {

        //    if (validationResult.HasFailures())
        //    validationResult = validationResult.Validate();

        //    var validationResult = inMemoryResourceDefinitions.Validate();
        //    loader.Load(topProjectModel);
        //    var loader = new top
        //{

        //public void LoadValidation(TopProjectModel topProjectModel)
        //    CreateActivities(topProjectModel.SecondaryActivities);
        //    CreateAreas(topProjectModel.ProductionAreas);
        //    CreateBuffers(topProjectModel.Buffers);
        //    CreateProcesses(topProjectModel.Processes);
        //    CreateWorkstationClasses(topProjectModel.WorkstationClasses);
        //    CreateWorkstationGroups(topProjectModel.WorkstationGroups);
        //    CreateWorkstations(topProjectModel.Workstations);
        //    CreateOperators(topProjectModel.Operators);
        //    CreateOperatorAlgorithms(topProjectModel.OperatorActivities);
        //    CreateOperatorAlgorithms(topProjectModel.OperatorPresences);
        //    CreateExtensionUnits(topProjectModel.ExtensionUnits);
        //}
    }
}