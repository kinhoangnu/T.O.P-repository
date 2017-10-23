using System.Collections.ObjectModel;

namespace Your
{
    public class ProdAreaList
    {
        public static ObservableCollection<ProdArea> ProdAreas { get; set; }

        public static ObservableCollection<ProdArea> GetProdAreaList()
        {
            return ProdAreas;
        }

        public static ProdArea GetAProdArea(string s)
        {
            foreach (var p in ProdAreas)
            {
                if (p.Uuid == s)
                {
                    return p;
                }
            }
            return null;
        }

        public ProdArea ReturnAProdArea(string s)
        {
            foreach (var p in ProdAreas)
            {
                if (p.PName == s)
                {
                    return p;
                }
            }

            return null;
        }

        public void DeleteAProdarea(ProdArea b)
        {
            ProdAreas.Remove(b);
        }

        private static void GenerateAProdarea()
        {
            //ProdAreas = new ObservableCollection<ProdArea>
            //{
            //    new ProdArea{P_name = "Receiving XDock",P_description="Inbound Production Area Pallet Receiving Normal", P_comID="ACPReceiving",P_Type="Inbound"},
            //    new ProdArea{P_name="ACP",P_description="Outbound Production Area ACP",P_comID="ACP",P_Type="Outbound"},
            //    new ProdArea{P_name="NC",P_description="Outbound Production Area NC Picking",P_comID="NC",P_Type="Outbound"},
            //    new ProdArea{P_name="Bulk",P_description="Outbound Production Area Bulk Picking",P_comID="Bulk",P_Type="Outbound"},
            //    new ProdArea{P_name="Full Pallet Picking",P_description="Outbound Production Area Full Pallet Picking",P_comID="FullPalletPicking",P_Type="Outbound"},
            //    new ProdArea{P_name="XDock",P_description="Outbound Production Area Xdock",P_comID="XDock",P_Type="Outbound"},
            //    new ProdArea{P_name="NC",P_description="Inbound Production Area NC Picking",P_comID="NC",P_Type="Inbound"},
            //    new ProdArea{P_name="Full Pallet Picking",P_description="Inbound Production Area Full Pallet Picking",P_comID="FullPalletPicking",P_Type="Inbound"}            
            //};
        }
    }
}