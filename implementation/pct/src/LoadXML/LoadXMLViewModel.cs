using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.ComponentModel;

using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.IO;
using System.Windows.Forms;

using Configurations;

using com.vanderlande.wpf;
using FM.Top.TopIntModel;
using FM.Top.TopIntTypes;

namespace Your
{
    public class LoadXMLViewModel : ContentViewModel
    {
        public RelayCommand LoadCommand { get; set; }
        XmlDocument doc = new XmlDocument();
        ConfigurationService con = new ConfigurationService();
        XmlReaderSettings setting = new XmlReaderSettings();
        string path = string.Empty;
        string xmlInputData = string.Empty;
        string xmlOutputData = string.Empty;

        public LoadXMLViewModel()
        {
            this.LoadCommand = new RelayCommand((obj) => Load());

        }

        /// <summary>
        /// Load the XML file
        /// </summary>
        public void Load()
        {
            var topConfigurationObject = new TopProjectModel();
            OpenFileDialog openfiledialog = new OpenFileDialog();
            if (openfiledialog.ShowDialog() == DialogResult.OK)
            {
                var path = openfiledialog.FileName;
                var serializer = new XmlSerializer(typeof(TopProjectModel));
                using (XmlReader reader = XmlReader.Create(path))
                {
                    topConfigurationObject = (TopProjectModel)serializer.Deserialize(reader);
                }
                #region Import Production Area
                foreach (FM.Top.TopIntTypes.ProductionArea p in topConfigurationObject.ProductionAreas)
                {
                    ProdAreaList.ProdAreas.Add(new ProdArea()
                        {
                            P_name = p.ObjectIdentification.Name,
                            P_comID = p.CommunicationId,
                            P_description = p.ObjectIdentification.Description,
                            P_type = p.ProductionType.ToString(),
                            Uuid = p.ObjectIdentification.UUID
                });
                }
                #endregion
                #region Import Buffer
                foreach (FM.Top.TopIntTypes.Buffer b in topConfigurationObject.Buffers)
                {
                    BufferList.Buffers.Add(new Buffer()
                    {
                        B_comID = b.CommunicationId,
                        B_description = b.ObjectIdentification.Description,
                        B_name = b.ObjectIdentification.Name,
                        B_unit = b.Unit,
                        Uuid = b.ObjectIdentification.UUID
                    });
                }
                #endregion
                #region Import Process
                foreach (FM.Top.TopIntTypes.Process pc in topConfigurationObject.Processes)
                {
                    ProcessList.Processes.Add(new Process() 
                    {
                        PC_comID = pc.CommunicationId,
                        PC_description = pc.ObjectIdentification.Description,
                        PC_name = pc.ObjectIdentification.Name,
                        Uuid = pc.ObjectIdentification.UUID,
                        InbufferRef = BufferList.GetABuffer(pc.InBuffer),
                        OutbufferRef = BufferList.GetABuffer(string.Join("",pc.OutBuffers)),
                        ProdRef = ProdAreaList.GetAProdArea(pc.ProductionAreaRef),
                        ExclFromKPI = pc.ExcludeFromKPIs,
                        IsReplenished = pc.IsReplenishment
                    });
                }
                #endregion
                #region Import Secondary Activity
                foreach (FM.Top.TopIntTypes.SecondaryActivity s in topConfigurationObject.SecondaryActivities)
                {
                    SecondaryActivityList.SecondaryActivities.Add(new SecondaryActivity()
                    {
                        SC_comID = s.CommunicationId,
                        SC_description = s.ObjectIdentification.Description,
                        SC_name = s.ObjectIdentification.Name,
                        Uuid = s.ObjectIdentification.UUID
                    });
                }
                #endregion
                #region Import Workstation group
                foreach (FM.Top.TopIntTypes.WorkstationGroup wg in topConfigurationObject.WorkstationGroups)
                {
                    WorkstationGroupList.WorkstationGroups.Add(new WorkstationGroup()
                    {
                        WG_description = wg.ObjectIdentification.Description,
                        WG_name = wg.ObjectIdentification.Name,
                        Uuid = wg.ObjectIdentification.UUID
                    });
                }
                #endregion
                #region Import Workstation class
                foreach (FM.Top.TopIntTypes.WorkstationClass wc in topConfigurationObject.WorkstationClasses)
                {
                    WorkstationClassList.WorkstationClasses.Add(new WorkstationClass()
                    {
                        WC_handlingType = wc.HandlingType.ToString(),
                        WC_name = wc.ObjectIdentification.Name,
                        WC_type = wc.WorkstationType,
                        Uuid = wc.ObjectIdentification.UUID,
                        ProcessRef = ProcessList.GetAProcess(wc.ProcessRef.ToString())
                        //SecondaryactivityRef = SecondaryActivityList.GetASecondaryActivity(string.Join("", Array.ConvertAll(wc.SecondaryActivities, x => x.ToString())))
                    });
                }
                #endregion
                #region Import Workstation
                foreach (FM.Top.TopIntTypes.Workstation w in topConfigurationObject.Workstations)
                {
                    if (w.WorkstationGroupRef != null)
                    {
                        WorkstationList.Workstations.Add(new Workstation()
                            {
                                W_comID = w.CommunicationId,
                                W_description = w.ObjectIdentification.Description,
                                W_name = w.ObjectIdentification.Name,
                                Uuid = w.ObjectIdentification.UUID,
                                WorkstationclassRef = WorkstationClassList.GetAWorkstationClass(w.WorkstationClassRef.ToString()),
                                WorkstationgroupRef = WorkstationGroupList.GetAWorkstationGroup(w.WorkstationGroupRef.ToString())
                            });
                    }
                    else
                    {
                        WorkstationList.Workstations.Add(new Workstation()
                        {
                            W_comID = w.CommunicationId,
                            W_description = w.ObjectIdentification.Description,
                            W_name = w.ObjectIdentification.Name,
                            Uuid = w.ObjectIdentification.UUID,
                            WorkstationclassRef = WorkstationClassList.GetAWorkstationClass(w.WorkstationClassRef.ToString())
                        });
                    }
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

    }
}
