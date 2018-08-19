using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMonify.Models
{
    public enum OperationCategoryEnum {
        Hygiene,
        Food,
        Accomodation,
        Health,
        Cafe,
        Car,
        Clothes,
        Pets,
        Presents,
        Entertainments,
        Communication,
        Sports,
        Taxi,
        Transport,
        Deposits,
        Salary,
        Saving,
        Transaction
    }

    class OperationCategory
    {
        static int iterator = 0;
        int id;
        string name;
        int operationTypeIndex;

        public OperationCategory()
        {
            id = iterator++;
        }

        public int Id { get => id; private set => id = value; }
        public string Name {
            get => name;
            set => name = value;
        }
        public int OperationTypeIndex {
            get => operationTypeIndex;
            set => operationTypeIndex = value;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
