using Castle.ActiveRecord;
using Membrane.Models.Interfaces;

namespace Membrane.Models
{
    public class ModelBase : IModelBase
    {
        private int id;

        [PrimaryKey (PrimaryKeyType.Native, Access = PropertyAccess.NosetterLowercase )]
        public int Id
        {
            get { return id; }
        }
    }
}