using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Your
{
    public class ProdAreaList
    {
        private static ObservableCollection<ProdArea> _prodareas;
        public static ObservableCollection<ProdArea> ProdAreas
        {
            get
            {
                return _prodareas;
            }
            set
            {
                //ChangeProperty(ref _buffers, value);
                _prodareas = value;
            }
        }
        public ProdAreaList()
        {
            ProdAreas = new ObservableCollection<ProdArea>
            {
                new ProdArea{P_objName = "Receiving XDock",P_description="Inbound Production Area Pallet Receiving Normal", P_ComID="ACPReceiving",P_Type="Inbound"},
                new ProdArea{P_objName="ACP",P_description="Outbound Production Area ACP",P_ComID="ACP",P_Type="Outbound"},
                new ProdArea{P_objName="NC",P_description="Outbound Production Area NC Picking",P_ComID="NC",P_Type="Outbound"},
                new ProdArea{P_objName="Bulk",P_description="Outbound Production Area Bulk Picking",P_ComID="Bulk",P_Type="Outbound"},
                new ProdArea{P_objName="Full Pallet Picking",P_description="Outbound Production Area Full Pallet Picking",P_ComID="FullPalletPicking",P_Type="Outbound"},
                new ProdArea{P_objName="XDock",P_description="Outbound Production Area Xdock",P_ComID="XDock",P_Type="Outbound"},
                new ProdArea{P_objName="NC",P_description="Inbound Production Area NC Picking",P_ComID="NC",P_Type="Inbound"},
                new ProdArea{P_objName="Full Pallet Picking",P_description="Inbound Production Area Full Pallet Picking",P_ComID="FullPalletPicking",P_Type="Inbound"}            
            };
        }

        private static void generateAProdarea()
        {
            ProdAreas = new ObservableCollection<ProdArea>
            {
                new ProdArea{P_objName = "Receiving XDock",P_description="Inbound Production Area Pallet Receiving Normal", P_ComID="ACPReceiving",P_Type="Inbound"},
                new ProdArea{P_objName="ACP",P_description="Outbound Production Area ACP",P_ComID="ACP",P_Type="Outbound"},
                new ProdArea{P_objName="NC",P_description="Outbound Production Area NC Picking",P_ComID="NC",P_Type="Outbound"},
                new ProdArea{P_objName="Bulk",P_description="Outbound Production Area Bulk Picking",P_ComID="Bulk",P_Type="Outbound"},
                new ProdArea{P_objName="Full Pallet Picking",P_description="Outbound Production Area Full Pallet Picking",P_ComID="FullPalletPicking",P_Type="Outbound"},
                new ProdArea{P_objName="XDock",P_description="Outbound Production Area Xdock",P_ComID="XDock",P_Type="Outbound"},
                new ProdArea{P_objName="NC",P_description="Inbound Production Area NC Picking",P_ComID="NC",P_Type="Inbound"},
                new ProdArea{P_objName="Full Pallet Picking",P_description="Inbound Production Area Full Pallet Picking",P_ComID="FullPalletPicking",P_Type="Inbound"}            
            };
        }

        public static ObservableCollection<ProdArea> GetProdAreaList()
        {
            return ProdAreas;
        }

        public static ProdArea GetAProdArea(int n)
        {
            generateAProdarea();
            return ProdAreas.ElementAt(n); 
        }

        public ProdArea ReturnAProdArea(string s)
        {
            foreach (ProdArea p in ProdAreas)
            {
                if (p.P_objName == s)
                    return p;
            }

            return null;
        }

        public void DeleteAProdarea(ProdArea b)
        {
            ProdAreas.Remove(b);
        }

    }
}
