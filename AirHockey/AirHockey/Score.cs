using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirHockey
{
    public class Score
    {
        private int Value;

        public Score()
        {
            this.Value = 0;
        }

        public int Points
        {
            get
            {
                return this.Value;
            }
            set
            {
                this.Value = value;
            }
        }
    }
}
