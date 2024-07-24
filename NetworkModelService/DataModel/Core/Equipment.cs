using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class Equipment : PowerSystemResource
    {
        private long equipmentContainer = 0;

        public Equipment(long globalId) : base(globalId)
        {
        }

        public long EquipmentContainer { get => equipmentContainer; set => equipmentContainer = value; }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                Equipment x = (Equipment)obj;
                return (x.equipmentContainer == this.equipmentContainer);
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
        public override bool HasProperty(ModelCode property)
        {
            switch (property)
            {
                case ModelCode.EQUIPMENT_EQUIPMENTCONTAINER:
                    return true;

                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.EQUIPMENT_EQUIPMENTCONTAINER:
                    prop.SetValue(equipmentContainer);
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
                case ModelCode.EQUIPMENT_EQUIPMENTCONTAINER:
                    equipmentContainer = property.AsReference();
                    break;
                default:
                    base.SetProperty(property);
                    break;
            }
        }

        #endregion IAccess implementation

        #region IReference implementation

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (equipmentContainer != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
            {
                references[ModelCode.EQUIPMENT_EQUIPMENTCONTAINER] = new List<long>();
                references[ModelCode.EQUIPMENT_EQUIPMENTCONTAINER].Add(equipmentContainer);
            }

            base.GetReferences(references, refType);
        }

        #endregion IReference implementation	


    }
}
