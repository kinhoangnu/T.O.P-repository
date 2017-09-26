﻿using System;
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
                new ProdArea{P_name = "Receiving XDock",P_description="Inbound Production Area Pallet Receiving Normal", P_comID="ACPReceiving",P_Type="Inbound"},
                new ProdArea{P_name="ACP",P_description="Outbound Production Area ACP",P_comID="ACP",P_Type="Outbound"},
                new ProdArea{P_name="NC",P_description="Outbound Production Area NC Picking",P_comID="NC",P_Type="Outbound"},
                new ProdArea{P_name="Bulk",P_description="Outbound Production Area Bulk Picking",P_comID="Bulk",P_Type="Outbound"},
                new ProdArea{P_name="Full Pallet Picking",P_description="Outbound Production Area Full Pallet Picking",P_comID="FullPalletPicking",P_Type="Outbound"},
                new ProdArea{P_name="XDock",P_description="Outbound Production Area Xdock",P_comID="XDock",P_Type="Outbound"},
                new ProdArea{P_name="NC",P_description="Inbound Production Area NC Picking",P_comID="NC",P_Type="Inbound"},
                new ProdArea{P_name="Full Pallet Picking",P_description="Inbound Production Area Full Pallet Picking",P_comID="FullPalletPicking",P_Type="Inbound"}             
            };
        }

        private static void generateAProdarea()
        {
            ProdAreas = new ObservableCollection<ProdArea>
            {
                new ProdArea{P_name = "Receiving XDock",P_description="Inbound Production Area Pallet Receiving Normal", P_comID="ACPReceiving",P_Type="Inbound"},
                new ProdArea{P_name="ACP",P_description="Outbound Production Area ACP",P_comID="ACP",P_Type="Outbound"},
                new ProdArea{P_name="NC",P_description="Outbound Production Area NC Picking",P_comID="NC",P_Type="Outbound"},
                new ProdArea{P_name="Bulk",P_description="Outbound Production Area Bulk Picking",P_comID="Bulk",P_Type="Outbound"},
                new ProdArea{P_name="Full Pallet Picking",P_description="Outbound Production Area Full Pallet Picking",P_comID="FullPalletPicking",P_Type="Outbound"},
                new ProdArea{P_name="XDock",P_description="Outbound Production Area Xdock",P_comID="XDock",P_Type="Outbound"},
                new ProdArea{P_name="NC",P_description="Inbound Production Area NC Picking",P_comID="NC",P_Type="Inbound"},
                new ProdArea{P_name="Full Pallet Picking",P_description="Inbound Production Area Full Pallet Picking",P_comID="FullPalletPicking",P_Type="Inbound"}            
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
                if (p.P_name == s)
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
