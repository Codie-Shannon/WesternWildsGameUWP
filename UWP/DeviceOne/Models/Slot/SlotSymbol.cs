namespace DeviceOne.Models
{
    public class SlotSymbol
    {
        // Name
        // ========================================================
        // ========================================================
        private string _name { get; set; }

        public string Name { get => _name; set => _name = value; }


        // Double
        // ========================================================
        // ========================================================
        // Double Cash
        private int _doubleCash { get; set; }

        public int DoubleCash { get => _doubleCash; set => _doubleCash = value; }


        // Double Free Spins
        private int _doubleFreeSpins { get; set; }

        public int DoubleFreeSpins { get => _doubleFreeSpins; set => _doubleFreeSpins = value; }


        // Triple
        // ========================================================
        // ========================================================
        // Triple Cash
        private int _tripleCash { get; set; }

        public int TripleCash { get => _tripleCash; set => _tripleCash = value; }


        // Triple Free Spins
        private int _tripleFreeSpins { get; set; }

        public int TripleFreeSpins { get => _tripleFreeSpins; set => _tripleFreeSpins = value; }
    }
}
