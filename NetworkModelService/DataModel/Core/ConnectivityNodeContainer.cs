using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class ConnectivityNodeContainer : PowerSystemResource
    {
        private List<long> connectivityNodes = new List<long>();

        public ConnectivityNodeContainer(long globalId) : base(globalId)
        {
        }

        public List<long> ConnectivityNodes { get => connectivityNodes; set => connectivityNodes = value; }


        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                ConnectivityNodeContainer x = (ConnectivityNodeContainer)obj;
                return (CompareHelper.CompareLists(x.connectivityNodes, this.connectivityNodes, true));
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #region IAccess implementation

        public override bool HasProperty(ModelCode t)
        {
            switch (t)
            {
                case ModelCode.CONNECTIVITYNODECONTAINER_CNS:
                    return true;

                default:
                    return base.HasProperty(t);
            }
        }

        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.CONNECTIVITYNODECONTAINER_CNS:
                    prop.SetValue(connectivityNodes);
                    break;

                default:
                    base.GetProperty(prop);
                    break;
            }
        }

        public override void SetProperty(Property property)
        {
            switch (property.Id)
            {
                default:
                    base.SetProperty(property);
                    break;
            }
        }

        #endregion IAccess implementation




    }
}
