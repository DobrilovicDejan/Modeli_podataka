using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class Bay : EquipmentContainer
    {
        private bool bayEnergyMeasFlag;
        private bool bayPowerMeasFlag;

        public Bay(long globalId) : base(globalId)
        {
        }

        public bool BayEnergyMeasFlag { get => bayEnergyMeasFlag; set => bayEnergyMeasFlag = value; }
        public bool BayPowerMeasFlag { get => bayPowerMeasFlag; set => bayPowerMeasFlag = value; }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                Bay x = (Bay)obj;
                return (
                        x.bayPowerMeasFlag == this.bayPowerMeasFlag
                     && x.bayEnergyMeasFlag == this.bayEnergyMeasFlag);
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
                case ModelCode.BAY_BAYENERGYMEASFLAG:
                case ModelCode.BAY_BAYPOWERMEASFLAG:
                    return true;

                default:
                    return base.HasProperty(t);
            }
        }

        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.BAY_BAYENERGYMEASFLAG:
                    prop.SetValue(bayEnergyMeasFlag);
                    break;

                case ModelCode.BAY_BAYPOWERMEASFLAG:
                    prop.SetValue(bayPowerMeasFlag);
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
                case ModelCode.BAY_BAYENERGYMEASFLAG:
                    bayEnergyMeasFlag = property.AsBool();
                    break;
                case ModelCode.BAY_BAYPOWERMEASFLAG:
                    bayPowerMeasFlag = property.AsBool();
                    break;

                default:
                    base.SetProperty(property);
                    break;
            }
        }

        #endregion IAccess implementation

    }
}
