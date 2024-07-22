namespace FTN.ESI.SIMES.CIM.CIMAdapter.Importer
{
	using FTN.Common;
    using System;
    using System.Threading.Tasks;

	/// <summary>
	/// PowerTransformerConverter has methods for populating
	/// ResourceDescription objects using PowerTransformerCIMProfile_Labs objects.
	/// </summary>
	public static class PowerTransformerConverter
	{

		#region Populate ResourceDescription

		public static void PopulateIdentifiedObjectProperties(FTN.IdentifiedObject cimIdentifiedObject, ResourceDescription rd)
		{

		}

		public static void PopulatePowerSystemResourceProperties(FTN.PowerSystemResource cimPowerSystemResource, ResourceDescription rd)
		{
			if ((cimPowerSystemResource != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimPowerSystemResource, rd);
			}
		}

		public static void PopulateConnectivityNodeProperties(FTN.ConnectivityNode cimConnectivityNode, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimConnectivityNode != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimConnectivityNode, rd);

				if (cimConnectivityNode.DescriptionHasValue)
				{
					rd.AddProperty(new Property(ModelCode.CONNECTIVITYNODE_DESCRIPTION, cimConnectivityNode.Description));
				}
				if (cimConnectivityNode.ConnectivityNodeContainerHasValue)
				{
					long gid = importHelper.GetMappedGID(cimConnectivityNode.ConnectivityNodeContainer.ID);
					if (gid > 0)
					{
						report.Report.Append("WARNING: Convert ").Append(cimConnectivityNode.GetType().ToString()).Append(" rdfID = \"").Append(cimConnectivityNode.ID);
						report.Report.Append("\" - Failed to set reference to IntervalSchedule: rdfID \"").Append(cimConnectivityNode.ConnectivityNodeContainer.ID).AppendLine(" \" is not mapped to GID!");
					}
					rd.AddProperty(new Property(ModelCode.CONNECTIVITYNODE_CNCONTAINER, gid));
				}
			}
		}

		public static void PopulateTerminalProperties(FTN.Terminal cimTerminal, ResourceDescription rd, ImportHelper importerHelper, TransformAndLoadReport report)
		{
			if ((cimTerminal != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimTerminal, rd);

				if (cimTerminal.ConductingEquipmentHasValue)
				{
					long gid = importerHelper.GetMappedGID(cimTerminal.ConductingEquipment.ID);
					if (gid > 0)
					{
						report.Report.Append("WARNING: Convert ").Append(cimTerminal.GetType().ToString()).Append(" rdfID = \"").Append(cimTerminal.ID);
						report.Report.Append("\" - Failed to set reference to IntervalSchedule: rdfID \"").Append(cimTerminal.ConductingEquipment.ID).AppendLine(" \" is not mapped to GID!");
					}
					rd.AddProperty(new Property(ModelCode.TERMINAL_CONDUCTINGEQUIPMENT, gid));
				}
				if (cimTerminal.ConnectivityNodeHasValue)
				{
					long gid = importerHelper.GetMappedGID(cimTerminal.ConnectivityNode.ID);
					if (gid > 0)
					{
						report.Report.Append("WARNING: Convert ").Append(cimTerminal.GetType().ToString()).Append(" rdfID = \"").Append(cimTerminal.ID);
						report.Report.Append("\" - Failed to set reference to IntervalSchedule: rdfID \"").Append(cimTerminal.ConnectivityNode.ID).AppendLine(" \" is not mapped to GID!");
					}
					rd.AddProperty(new Property(ModelCode.TERMINAL_CONNECTIVITYNODE, gid));
				}
            }

        }

        public static void PopulateEquipmentProperties(FTN.Equipment equipment, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((equipment != null) && (rd != null))
			{
				PowerTransformerConverter.PopulatePowerSystemResourceProperties(equipment, rd);

				if (equipment.EquipmentContainerHasValue)
				{
					long gid = importHelper.GetMappedGID(equipment.EquipmentContainer.ID);
					if (gid > 0)
					{
						report.Report.Append("WARNING: Convert ").Append(equipment.GetType().ToString()).Append(" rdfID = \"").Append(equipment.ID);
						report.Report.Append("\" - Failed to set reference to IntervalSchedule: rdfID \"").Append(equipment.EquipmentContainer.ID).AppendLine(" \" is not mapped to GID!");
					}
					rd.AddProperty(new Property(ModelCode.TERMINAL_CONDUCTINGEQUIPMENT, gid));
				}
			}
		}

		public static void PopulateConnectivityNodeContainerProperties(FTN.ConnectivityNodeContainer connectivityNodeContainer, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((connectivityNodeContainer != null) && (rd != null))
			{
				PowerTransformerConverter.PopulatePowerSystemResourceProperties(connectivityNodeContainer, rd);
			}
		}

		public static void PopulateConductingEquipmentProperties(FTN.ConductingEquipment conductingEquipment, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((conductingEquipment != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateEquipmentProperties(conductingEquipment, rd, importHelper, report);
			}
		}


		public static void PopulateEquipmentContainerProperties(FTN.EquipmentContainer equipmentContainer, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((equipmentContainer != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateConnectivityNodeContainerProperties(equipmentContainer, rd, importHelper, report);
			}
		}

		public static void PopulateBayProperties(FTN.Bay cimBay, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if((cimBay != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateEquipmentContainerProperties(cimBay, rd, importHelper, report);


			}
		}







        /*
		public static void PopulateLocationProperties(FTN.Location cimLocation, ResourceDescription rd)
		{
			if ((cimLocation != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimLocation, rd);

				if (cimLocation.CategoryHasValue)
				{
					rd.AddProperty(new Property(ModelCode.LOCATION_CATEGORY, cimLocation.Category));
				}
				if (cimLocation.CorporateCodeHasValue)
				{
					rd.AddProperty(new Property(ModelCode.LOCATION_CORPORATECODE, cimLocation.CorporateCode));
				}
			}
		}

		public static void PopulatePowerSystemResourceProperties(FTN.PowerSystemResource cimPowerSystemResource, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimPowerSystemResource != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimPowerSystemResource, rd);

				if (cimPowerSystemResource.CustomTypeHasValue)
				{
					rd.AddProperty(new Property(ModelCode.PSR_CUSTOMTYPE, cimPowerSystemResource.CustomType));
				}
				if (cimPowerSystemResource.LocationHasValue)
				{
					long gid = importHelper.GetMappedGID(cimPowerSystemResource.Location.ID);
					if (gid < 0)
					{
						report.Report.Append("WARNING: Convert ").Append(cimPowerSystemResource.GetType().ToString()).Append(" rdfID = \"").Append(cimPowerSystemResource.ID);
						report.Report.Append("\" - Failed to set reference to Location: rdfID \"").Append(cimPowerSystemResource.Location.ID).AppendLine(" \" is not mapped to GID!");
					}
					rd.AddProperty(new Property(ModelCode.PSR_LOCATION, gid));
				}
			}
		}

		public static void PopulateBaseVoltageProperties(FTN.BaseVoltage cimBaseVoltage, ResourceDescription rd)
		{
			if ((cimBaseVoltage != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimBaseVoltage, rd);

				if (cimBaseVoltage.NominalVoltageHasValue)
				{
					rd.AddProperty(new Property(ModelCode.BASEVOLTAGE_NOMINALVOLTAGE, cimBaseVoltage.NominalVoltage));
				}
			}
		}

		public static void PopulateEquipmentProperties(FTN.Equipment cimEquipment, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimEquipment != null) && (rd != null))
			{
				PowerTransformerConverter.PopulatePowerSystemResourceProperties(cimEquipment, rd, importHelper, report);

				if (cimEquipment.PrivateHasValue)
				{
					rd.AddProperty(new Property(ModelCode.EQUIPMENT_ISPRIVATE, cimEquipment.Private));
				}
				if (cimEquipment.IsUndergroundHasValue)
				{
					rd.AddProperty(new Property(ModelCode.EQUIPMENT_ISUNDERGROUND, cimEquipment.IsUnderground));
				}
			}
		}

		public static void PopulateConductingEquipmentProperties(FTN.ConductingEquipment cimConductingEquipment, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimConductingEquipment != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateEquipmentProperties(cimConductingEquipment, rd, importHelper, report);

				if (cimConductingEquipment.PhasesHasValue)
				{
					rd.AddProperty(new Property(ModelCode.CONDEQ_PHASES, (short)GetDMSPhaseCode(cimConductingEquipment.Phases)));
				}
				if (cimConductingEquipment.RatedVoltageHasValue)
				{
					rd.AddProperty(new Property(ModelCode.CONDEQ_RATEDVOLTAGE, cimConductingEquipment.RatedVoltage));
				}
				if (cimConductingEquipment.BaseVoltageHasValue)
				{
					long gid = importHelper.GetMappedGID(cimConductingEquipment.BaseVoltage.ID);
					if (gid < 0)
					{
						report.Report.Append("WARNING: Convert ").Append(cimConductingEquipment.GetType().ToString()).Append(" rdfID = \"").Append(cimConductingEquipment.ID);
						report.Report.Append("\" - Failed to set reference to BaseVoltage: rdfID \"").Append(cimConductingEquipment.BaseVoltage.ID).AppendLine(" \" is not mapped to GID!");
					}
					rd.AddProperty(new Property(ModelCode.CONDEQ_BASVOLTAGE, gid));
				}
			}
		}

		public static void PopulatePowerTransformerProperties(FTN.PowerTransformer cimPowerTransformer, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimPowerTransformer != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateEquipmentProperties(cimPowerTransformer, rd, importHelper, report);

				if (cimPowerTransformer.FunctionHasValue)
				{
					rd.AddProperty(new Property(ModelCode.POWERTR_FUNC, (short)GetDMSTransformerFunctionKind(cimPowerTransformer.Function)));
				}
				if (cimPowerTransformer.AutotransformerHasValue)
				{
					rd.AddProperty(new Property(ModelCode.POWERTR_AUTO, cimPowerTransformer.Autotransformer));
				}
			}
		}

		public static void PopulateTransformerWindingProperties(FTN.TransformerWinding cimTransformerWinding, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimTransformerWinding != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateConductingEquipmentProperties(cimTransformerWinding, rd, importHelper, report);

				if (cimTransformerWinding.ConnectionTypeHasValue)
				{
					rd.AddProperty(new Property(ModelCode.POWERTRWINDING_CONNTYPE, (short)GetDMSWindingConnection(cimTransformerWinding.ConnectionType)));
				}
				if (cimTransformerWinding.GroundedHasValue)
				{
					rd.AddProperty(new Property(ModelCode.POWERTRWINDING_GROUNDED, cimTransformerWinding.Grounded));
				}
				if (cimTransformerWinding.RatedSHasValue)
				{
					rd.AddProperty(new Property(ModelCode.POWERTRWINDING_RATEDS, cimTransformerWinding.RatedS));
				}
				if (cimTransformerWinding.RatedUHasValue)
				{
					rd.AddProperty(new Property(ModelCode.POWERTRWINDING_RATEDU, cimTransformerWinding.RatedU));
				}
				if (cimTransformerWinding.WindingTypeHasValue)
				{
					rd.AddProperty(new Property(ModelCode.POWERTRWINDING_WINDTYPE, (short)GetDMSWindingType(cimTransformerWinding.WindingType)));
				}
				if (cimTransformerWinding.PhaseToGroundVoltageHasValue)
				{
					rd.AddProperty(new Property(ModelCode.POWERTRWINDING_PHASETOGRNDVOLTAGE, cimTransformerWinding.PhaseToGroundVoltage));
				}
				if (cimTransformerWinding.PhaseToPhaseVoltageHasValue)
				{
					rd.AddProperty(new Property(ModelCode.POWERTRWINDING_PHASETOPHASEVOLTAGE, cimTransformerWinding.PhaseToPhaseVoltage));
				}
				if (cimTransformerWinding.PowerTransformerHasValue)
				{
					long gid = importHelper.GetMappedGID(cimTransformerWinding.PowerTransformer.ID);
					if (gid < 0)
					{
						report.Report.Append("WARNING: Convert ").Append(cimTransformerWinding.GetType().ToString()).Append(" rdfID = \"").Append(cimTransformerWinding.ID);
						report.Report.Append("\" - Failed to set reference to PowerTransformer: rdfID \"").Append(cimTransformerWinding.PowerTransformer.ID).AppendLine(" \" is not mapped to GID!");
					}
					rd.AddProperty(new Property(ModelCode.POWERTRWINDING_POWERTRW, gid));
				}
			}
		}

		public static void PopulateWindingTestProperties(FTN.WindingTest cimWindingTest, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimWindingTest != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimWindingTest, rd);

				if (cimWindingTest.LeakageImpedanceHasValue)
				{
					rd.AddProperty(new Property(ModelCode.WINDINGTEST_LEAKIMPDN, cimWindingTest.LeakageImpedance));
				}
				if (cimWindingTest.LoadLossHasValue)
				{
					rd.AddProperty(new Property(ModelCode.WINDINGTEST_LOADLOSS, cimWindingTest.LoadLoss));
				}
				if (cimWindingTest.NoLoadLossHasValue)
				{
					rd.AddProperty(new Property(ModelCode.WINDINGTEST_NOLOADLOSS, cimWindingTest.NoLoadLoss));
				}
				if (cimWindingTest.PhaseShiftHasValue)
				{
					rd.AddProperty(new Property(ModelCode.WINDINGTEST_PHASESHIFT, cimWindingTest.PhaseShift));
				}
				if (cimWindingTest.LeakageImpedance0PercentHasValue)
				{
					rd.AddProperty(new Property(ModelCode.WINDINGTEST_LEAKIMPDN0PERCENT, cimWindingTest.LeakageImpedance0Percent));
				}
				if (cimWindingTest.LeakageImpedanceMaxPercentHasValue)
				{
					rd.AddProperty(new Property(ModelCode.WINDINGTEST_LEAKIMPDNMAXPERCENT, cimWindingTest.LeakageImpedanceMaxPercent));
				}
				if (cimWindingTest.LeakageImpedanceMinPercentHasValue)
				{
					rd.AddProperty(new Property(ModelCode.WINDINGTEST_LEAKIMPDNMINPERCENT, cimWindingTest.LeakageImpedanceMinPercent));
				}

				if (cimWindingTest.From_TransformerWindingHasValue)
				{
					long gid = importHelper.GetMappedGID(cimWindingTest.From_TransformerWinding.ID);
					if (gid < 0)
					{
						report.Report.Append("WARNING: Convert ").Append(cimWindingTest.GetType().ToString()).Append(" rdfID = \"").Append(cimWindingTest.ID);
						report.Report.Append("\" - Failed to set reference to TransformerWinding: rdfID \"").Append(cimWindingTest.From_TransformerWinding.ID).AppendLine(" \" is not mapped to GID!");
					}
					rd.AddProperty(new Property(ModelCode.WINDINGTEST_POWERTRWINDING, gid));
				}
			}
		}
		*/



        #endregion Populate ResourceDescription


    }
}
