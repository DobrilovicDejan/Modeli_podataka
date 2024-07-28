using System;
using System.Collections.Generic;
using CIM.Model;
using FTN.Common;
using FTN.ESI.SIMES.CIM.CIMAdapter.Manager;

namespace FTN.ESI.SIMES.CIM.CIMAdapter.Importer
{
	/// <summary>
	/// PowerTransformerImporter
	/// </summary>
	public class PowerTransformerImporter
	{
		/// <summary> Singleton </summary>
		private static PowerTransformerImporter ptImporter = null;
		private static object singletoneLock = new object();

		private ConcreteModel concreteModel;
		private Delta delta;
		private ImportHelper importHelper;
		private TransformAndLoadReport report;


		#region Properties
		public static PowerTransformerImporter Instance
		{
			get
			{
				if (ptImporter == null)
				{
					lock (singletoneLock)
					{
						if (ptImporter == null)
						{
							ptImporter = new PowerTransformerImporter();
							ptImporter.Reset();
						}
					}
				}
				return ptImporter;
			}
		}

		public Delta NMSDelta
		{
			get 
			{
				return delta;
			}
		}
		#endregion Properties


		public void Reset()
		{
			concreteModel = null;
			delta = new Delta();
			importHelper = new ImportHelper();
			report = null;
		}

		public TransformAndLoadReport CreateNMSDelta(ConcreteModel cimConcreteModel)
		{
			LogManager.Log("Importing PowerTransformer Elements...", LogLevel.Info);
			report = new TransformAndLoadReport();
			concreteModel = cimConcreteModel;
			delta.ClearDeltaOperations();

			if ((concreteModel != null) && (concreteModel.ModelMap != null))
			{
				try
				{
					// convert into DMS elements
					ConvertModelAndPopulateDelta();
				}
				catch (Exception ex)
				{
					string message = string.Format("{0} - ERROR in data import - {1}", DateTime.Now, ex.Message);
					LogManager.Log(message);
					report.Report.AppendLine(ex.Message);
					report.Success = false;
				}
			}
			LogManager.Log("Importing PowerTransformer Elements - END.", LogLevel.Info);
			return report;
		}

		/// <summary>
		/// Method performs conversion of network elements from CIM based concrete model into DMS model.
		/// </summary>
		private void ConvertModelAndPopulateDelta()
		{
			LogManager.Log("Loading elements and creating delta...", LogLevel.Info);

			//// import all concrete model types (DMSType enum)
			/*
			ImportBaseVoltages();
			ImportLocations();
			ImportPowerTransformers();
			ImportTransformerWindings();
			ImportWindingTests();
			*/

			ImportTerminal();
			ImportSeriesCompensator();
			ImportConnectivityNode();
			ImportDCLineSegment();
			ImportACLineSegment();
			ImportBay();

			LogManager.Log("Loading elements and creating delta completed.", LogLevel.Info);
		}

        private void ImportTerminal()
        {
            SortedDictionary<string, object> cimTerminals = concreteModel.GetAllObjectsOfType("FTN.Terminal");
            if (cimTerminals != null)
            {
                foreach (var cimPair in cimTerminals)
                {
                    FTN.Terminal cimTerminal = cimPair.Value as FTN.Terminal;

                    ResourceDescription rd = CreateTerminalDescription(cimTerminal);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("Terminal ID =").Append(cimTerminal.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("Terminal ID = ").Append(cimTerminal.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private void ImportConnectivityNode()
        {
            SortedDictionary<string, object> cimConnectivityNodes = concreteModel.GetAllObjectsOfType("FTN.ConnectivityNode");
            if (cimConnectivityNodes != null)
            {
                foreach (var cimPair in cimConnectivityNodes)
                {
                    FTN.ConnectivityNode cimConnectivityNode = cimPair.Value as FTN.ConnectivityNode;

                    ResourceDescription rd = CreateConnectivityNodeDescription(cimConnectivityNode);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("ConnectivityNode ID =").Append(cimConnectivityNode.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("ConnectivityNode ID = ").Append(cimConnectivityNode.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private void ImportSeriesCompensator()
        {
            SortedDictionary<string, object> cimSeriesCompensators = concreteModel.GetAllObjectsOfType("FTN.SeriesCompensator");
            if (cimSeriesCompensators != null)
            {
                foreach (var cimPair in cimSeriesCompensators)
                {
                    FTN.SeriesCompensator cimSeriesCompensator = cimPair.Value as FTN.SeriesCompensator;

                    ResourceDescription rd = CreateSeriesCompensatorDescription(cimSeriesCompensator);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("SeriesCompensator ID =").Append(cimSeriesCompensator.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("SeriesCompensator ID = ").Append(cimSeriesCompensator.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private void ImportBay()
        {
            SortedDictionary<string, object> cimBays = concreteModel.GetAllObjectsOfType("FTN.Bay");
            if (cimBays != null)
            {
                foreach (var cimPair in cimBays)
                {
                    FTN.Bay cimBay = cimPair.Value as FTN.Bay;

                    ResourceDescription rd = CreateBayDescription(cimBay);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("Bay ID =").Append(cimBay.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("Bay ID = ").Append(cimBay.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private void ImportACLineSegment()
        {
            SortedDictionary<string, object> cimACLineSegments = concreteModel.GetAllObjectsOfType("FTN.ACLineSegment");
            if (cimACLineSegments != null)
            {
                foreach (var cimPair in cimACLineSegments)
                {
                    FTN.ACLineSegment cimACLineSegment = cimPair.Value as FTN.ACLineSegment;

                    ResourceDescription rd = CreateACLineSegmentDescription(cimACLineSegment);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("ACLineSegment ID =").Append(cimACLineSegment.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("ACLineSegment ID = ").Append(cimACLineSegment.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }



		private void ImportDCLineSegment()
		{
			SortedDictionary<string, object> cimDCLineSegments = concreteModel.GetAllObjectsOfType("FTN.DCLineSegment");
			if(cimDCLineSegments != null )
			{
				foreach(var cimPair in cimDCLineSegments)
				{
					FTN.DCLineSegment cimDCLineSegment = cimPair.Value as FTN.DCLineSegment;
					
					ResourceDescription rd = CreateDCLineSegmentDescription(cimDCLineSegment);
					if(rd != null)
					{
						delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
						report.Report.Append("DCLineSegment ID =").Append(cimDCLineSegment.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
					else
					{
                        report.Report.Append("DCLineSegment ID = ").Append(cimDCLineSegment.ID).AppendLine(" FAILED to be converted");
                    }
                }
				report.Report.AppendLine();
			}
				
		}

        private ResourceDescription CreateDCLineSegmentDescription(FTN.DCLineSegment cimDCLineSegment)
        {
			ResourceDescription rd = null;
			if(cimDCLineSegment != null)
			{
				DMSType dmsType = DMSType.DCLINESEGMENT;

				long gid = ModelCodeHelper.CreateGlobalId(0, (short)dmsType, importHelper.CheckOutIndexForDMSType(dmsType));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimDCLineSegment.ID, gid);

				PowerTransformerConverter.PopulateDCLineSegmentProperties(cimDCLineSegment, rd, importHelper, report);
			}
			return rd;
        }

        private ResourceDescription CreateACLineSegmentDescription(FTN.ACLineSegment cimACLineSegment)
        {
            ResourceDescription rd = null;
            if (cimACLineSegment != null)
            {
                DMSType dmsType = DMSType.ACLINESEGMENT;

                long gid = ModelCodeHelper.CreateGlobalId(0, (short)dmsType, importHelper.CheckOutIndexForDMSType(dmsType));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimACLineSegment.ID, gid);

                PowerTransformerConverter.PopulateACLineSegmentProperties(cimACLineSegment, rd, importHelper, report);
            }
            return rd;
        }



        private ResourceDescription CreateBayDescription(FTN.Bay cimBay)
        {
            ResourceDescription rd = null;
            if (cimBay != null)
            {
                DMSType dmsType = DMSType.BAY;

                long gid = ModelCodeHelper.CreateGlobalId(0, (short)dmsType, importHelper.CheckOutIndexForDMSType(dmsType));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimBay.ID, gid);

                PowerTransformerConverter.PopulateBayProperties(cimBay, rd, importHelper, report);
            }
            return rd;
        }




        private ResourceDescription CreateSeriesCompensatorDescription(FTN.SeriesCompensator cimSeriesCompensator)
        {
            ResourceDescription rd = null;
            if (cimSeriesCompensator != null)
            {
                DMSType dmsType = DMSType.SERIESCOMPENSATOR;

                long gid = ModelCodeHelper.CreateGlobalId(0, (short)dmsType, importHelper.CheckOutIndexForDMSType(dmsType));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimSeriesCompensator.ID, gid);

                PowerTransformerConverter.PopulateSeriesCompensatorProperties(cimSeriesCompensator, rd, importHelper, report);
            }
            return rd;
        }



        private ResourceDescription CreateConnectivityNodeDescription(FTN.ConnectivityNode cimConnectivityNode)
        {
            ResourceDescription rd = null;
            if (cimConnectivityNode != null)
            {
                DMSType dmsType = DMSType.CONNECTIVITYNODE;

                long gid = ModelCodeHelper.CreateGlobalId(0, (short)dmsType, importHelper.CheckOutIndexForDMSType(dmsType));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimConnectivityNode.ID, gid);

                PowerTransformerConverter.PopulateConnectivityNodeProperties(cimConnectivityNode, rd, importHelper, report);
            }
            return rd;
        }




        private ResourceDescription CreateTerminalDescription(FTN.Terminal cimTerminal)
        {
            ResourceDescription rd = null;
            if (cimTerminal != null)
            {
                DMSType dmsType = DMSType.TERMINAL;

                long gid = ModelCodeHelper.CreateGlobalId(0, (short)dmsType, importHelper.CheckOutIndexForDMSType(dmsType));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimTerminal.ID, gid);

                PowerTransformerConverter.PopulateTerminalProperties(cimTerminal, rd, importHelper, report);
            }
            return rd;
        }




        /*
		#region Import
		private void ImportBaseVoltages()
		{
			SortedDictionary<string, object> cimBaseVoltages = concreteModel.GetAllObjectsOfType("FTN.BaseVoltage");
			if (cimBaseVoltages != null)
			{
				foreach (KeyValuePair<string, object> cimBaseVoltagePair in cimBaseVoltages)
				{
					FTN.BaseVoltage cimBaseVoltage = cimBaseVoltagePair.Value as FTN.BaseVoltage;

					ResourceDescription rd = CreateBaseVoltageResourceDescription(cimBaseVoltage);
					if (rd != null)
					{
						delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
						report.Report.Append("BaseVoltage ID = ").Append(cimBaseVoltage.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
					}
					else
					{
						report.Report.Append("BaseVoltage ID = ").Append(cimBaseVoltage.ID).AppendLine(" FAILED to be converted");
					}
				}
				report.Report.AppendLine();
			}
		}

		private ResourceDescription CreateBaseVoltageResourceDescription(FTN.BaseVoltage cimBaseVoltage)
		{
			ResourceDescription rd = null;
			if (cimBaseVoltage != null)
			{
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.BASEVOLTAGE, importHelper.CheckOutIndexForDMSType(DMSType.BASEVOLTAGE));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimBaseVoltage.ID, gid);

				////populate ResourceDescription
				PowerTransformerConverter.PopulateBaseVoltageProperties(cimBaseVoltage, rd);
			}
			return rd;
		}
		
		private void ImportLocations()
		{
			SortedDictionary<string, object> cimLocations = concreteModel.GetAllObjectsOfType("FTN.Location");
			if (cimLocations != null)
			{
				foreach (KeyValuePair<string, object> cimLocationPair in cimLocations)
				{
					FTN.Location cimLocation = cimLocationPair.Value as FTN.Location;

					ResourceDescription rd = CreateLocationResourceDescription(cimLocation);
					if (rd != null)
					{
						delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
						report.Report.Append("Location ID = ").Append(cimLocation.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
					}
					else
					{
						report.Report.Append("Location ID = ").Append(cimLocation.ID).AppendLine(" FAILED to be converted");
					}
				}
				report.Report.AppendLine();
			}
		}

		private ResourceDescription CreateLocationResourceDescription(FTN.Location cimLocation)
		{
			ResourceDescription rd = null;
			if (cimLocation != null)
			{
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.LOCATION, importHelper.CheckOutIndexForDMSType(DMSType.LOCATION));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimLocation.ID, gid);

				////populate ResourceDescription
				PowerTransformerConverter.PopulateLocationProperties(cimLocation, rd);
			}
			return rd;
		}

		private void ImportPowerTransformers()
		{
			SortedDictionary<string, object> cimPowerTransformers = concreteModel.GetAllObjectsOfType("FTN.PowerTransformer");
			if (cimPowerTransformers != null)
			{
				foreach (KeyValuePair<string, object> cimPowerTransformerPair in cimPowerTransformers)
				{
					FTN.PowerTransformer cimPowerTransformer = cimPowerTransformerPair.Value as FTN.PowerTransformer;

					ResourceDescription rd = CreatePowerTransformerResourceDescription(cimPowerTransformer);
					if (rd != null)
					{
						delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
						report.Report.Append("PowerTransformer ID = ").Append(cimPowerTransformer.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
					}
					else
					{
						report.Report.Append("PowerTransformer ID = ").Append(cimPowerTransformer.ID).AppendLine(" FAILED to be converted");
					}
				}
				report.Report.AppendLine();
			}
		}

		private ResourceDescription CreatePowerTransformerResourceDescription(FTN.PowerTransformer cimPowerTransformer)
		{
			ResourceDescription rd = null;
			if (cimPowerTransformer != null)
			{
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.POWERTR, importHelper.CheckOutIndexForDMSType(DMSType.POWERTR));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimPowerTransformer.ID, gid);

				////populate ResourceDescription
				PowerTransformerConverter.PopulatePowerTransformerProperties(cimPowerTransformer, rd, importHelper, report);
			}
			return rd;
		}

		private void ImportTransformerWindings()
		{
			SortedDictionary<string, object> cimTransformerWindings = concreteModel.GetAllObjectsOfType("FTN.TransformerWinding");
			if (cimTransformerWindings != null)
			{
				foreach (KeyValuePair<string, object> cimTransformerWindingPair in cimTransformerWindings)
				{
					FTN.TransformerWinding cimTransformerWinding = cimTransformerWindingPair.Value as FTN.TransformerWinding;

					ResourceDescription rd = CreateTransformerWindingResourceDescription(cimTransformerWinding);
					if (rd != null)
					{
						delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
						report.Report.Append("TransformerWinding ID = ").Append(cimTransformerWinding.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
					}
					else
					{
						report.Report.Append("TransformerWinding ID = ").Append(cimTransformerWinding.ID).AppendLine(" FAILED to be converted");
					}
				}
				report.Report.AppendLine();
			}
		}

		private ResourceDescription CreateTransformerWindingResourceDescription(FTN.TransformerWinding cimTransformerWinding)
		{
			ResourceDescription rd = null;
			if (cimTransformerWinding != null)
			{
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.POWERTRWINDING, importHelper.CheckOutIndexForDMSType(DMSType.POWERTRWINDING));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimTransformerWinding.ID, gid);

				////populate ResourceDescription
				PowerTransformerConverter.PopulateTransformerWindingProperties(cimTransformerWinding, rd, importHelper, report);
			}
			return rd;
		}

		private void ImportWindingTests()
		{
			SortedDictionary<string, object> cimWindingTests = concreteModel.GetAllObjectsOfType("FTN.WindingTest");
			if (cimWindingTests != null)
			{
				foreach (KeyValuePair<string, object> cimWindingTestPair in cimWindingTests)
				{
					FTN.WindingTest cimWindingTest = cimWindingTestPair.Value as FTN.WindingTest;

					ResourceDescription rd = CreateWindingTestResourceDescription(cimWindingTest);
					if (rd != null)
					{
						delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
						report.Report.Append("WindingTest ID = ").Append(cimWindingTest.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
					}
					else
					{
						report.Report.Append("WindingTest ID = ").Append(cimWindingTest.ID).AppendLine(" FAILED to be converted");
					}
				}
				report.Report.AppendLine();
			}
		}

		private ResourceDescription CreateWindingTestResourceDescription(FTN.WindingTest cimWindingTest)
		{
			ResourceDescription rd = null;
			if (cimWindingTest != null)
			{
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.WINDINGTEST, importHelper.CheckOutIndexForDMSType(DMSType.WINDINGTEST));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimWindingTest.ID, gid);

				////populate ResourceDescription
				PowerTransformerConverter.PopulateWindingTestProperties(cimWindingTest, rd, importHelper, report);
			}
			return rd;
		}
		#endregion Import

		*/
    }
}

