using System;

namespace AKTest.Model
{
    public class Entrants
    {
        public int? id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }

        public override bool Equals(object objectToCompare)
        {
            // Check for null and compare run-time types
            if (objectToCompare is null || !this.GetType().Equals(objectToCompare.GetType()))
            {
                return false;
            }
            else
            {
                Entrants itemm = (Entrants)objectToCompare;
                return (id == itemm.id);
            }
        }

        public override int GetHashCode()
        {
            return id.GetHashCode();
        }
    }
}
