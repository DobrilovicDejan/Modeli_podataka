//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FTN {
    using System;
    using FTN;
    
    
    /// A wire or combination of wires, with consistent electrical characteristics, building a single electrical system, used to carry alternating current between points in the power system.
    ///For symmetrical, transposed 3ph lines, it is sufficient to use  attributes of the line segment, which describe impedances and admittances for the entire length of the segment.  Additionally impedances can be computed by using length and associated per length impedances.
    public class ACLineSegment : Conductor {
        
        /// Zero sequence shunt (charging) susceptance, uniformly distributed, of the entire line section.
        private System.Single? cim_b0ch;
        
        private const bool isB0chMandatory = false;
        
        private const string _b0chPrefix = "cim";
        
        /// Positive sequence shunt (charging) susceptance, uniformly distributed, of the entire line section.  This value represents the full charging over the full length of the line.
        private System.Single? cim_bch;
        
        private const bool isBchMandatory = false;
        
        private const string _bchPrefix = "cim";
        
        /// Zero sequence shunt (charging) conductance, uniformly distributed, of the entire line section.
        private System.Single? cim_g0ch;
        
        private const bool isG0chMandatory = false;
        
        private const string _g0chPrefix = "cim";
        
        /// Positive sequence shunt (charging) conductance, uniformly distributed, of the entire line section.
        private System.Single? cim_gch;
        
        private const bool isGchMandatory = false;
        
        private const string _gchPrefix = "cim";
        
        /// Positive sequence series resistance of the entire line section.
        private System.Single? cim_r;
        
        private const bool isRMandatory = false;
        
        private const string _rPrefix = "cim";
        
        /// Zero sequence series resistance of the entire line section.
        private System.Single? cim_r0;
        
        private const bool isR0Mandatory = false;
        
        private const string _r0Prefix = "cim";
        
        /// Positive sequence series reactance of the entire line section.
        private System.Single? cim_x;
        
        private const bool isXMandatory = false;
        
        private const string _xPrefix = "cim";
        
        /// Zero sequence series reactance of the entire line section.
        private System.Single? cim_x0;
        
        private const bool isX0Mandatory = false;
        
        private const string _x0Prefix = "cim";
        
        public virtual float B0ch {
            get {
                return this.cim_b0ch.GetValueOrDefault();
            }
            set {
                this.cim_b0ch = value;
            }
        }
        
        public virtual bool B0chHasValue {
            get {
                return this.cim_b0ch != null;
            }
        }
        
        public static bool IsB0chMandatory {
            get {
                return isB0chMandatory;
            }
        }
        
        public static string B0chPrefix {
            get {
                return _b0chPrefix;
            }
        }
        
        public virtual float Bch {
            get {
                return this.cim_bch.GetValueOrDefault();
            }
            set {
                this.cim_bch = value;
            }
        }
        
        public virtual bool BchHasValue {
            get {
                return this.cim_bch != null;
            }
        }
        
        public static bool IsBchMandatory {
            get {
                return isBchMandatory;
            }
        }
        
        public static string BchPrefix {
            get {
                return _bchPrefix;
            }
        }
        
        public virtual float G0ch {
            get {
                return this.cim_g0ch.GetValueOrDefault();
            }
            set {
                this.cim_g0ch = value;
            }
        }
        
        public virtual bool G0chHasValue {
            get {
                return this.cim_g0ch != null;
            }
        }
        
        public static bool IsG0chMandatory {
            get {
                return isG0chMandatory;
            }
        }
        
        public static string G0chPrefix {
            get {
                return _g0chPrefix;
            }
        }
        
        public virtual float Gch {
            get {
                return this.cim_gch.GetValueOrDefault();
            }
            set {
                this.cim_gch = value;
            }
        }
        
        public virtual bool GchHasValue {
            get {
                return this.cim_gch != null;
            }
        }
        
        public static bool IsGchMandatory {
            get {
                return isGchMandatory;
            }
        }
        
        public static string GchPrefix {
            get {
                return _gchPrefix;
            }
        }
        
        public virtual float R {
            get {
                return this.cim_r.GetValueOrDefault();
            }
            set {
                this.cim_r = value;
            }
        }
        
        public virtual bool RHasValue {
            get {
                return this.cim_r != null;
            }
        }
        
        public static bool IsRMandatory {
            get {
                return isRMandatory;
            }
        }
        
        public static string RPrefix {
            get {
                return _rPrefix;
            }
        }
        
        public virtual float R0 {
            get {
                return this.cim_r0.GetValueOrDefault();
            }
            set {
                this.cim_r0 = value;
            }
        }
        
        public virtual bool R0HasValue {
            get {
                return this.cim_r0 != null;
            }
        }
        
        public static bool IsR0Mandatory {
            get {
                return isR0Mandatory;
            }
        }
        
        public static string R0Prefix {
            get {
                return _r0Prefix;
            }
        }
        
        public virtual float X {
            get {
                return this.cim_x.GetValueOrDefault();
            }
            set {
                this.cim_x = value;
            }
        }
        
        public virtual bool XHasValue {
            get {
                return this.cim_x != null;
            }
        }
        
        public static bool IsXMandatory {
            get {
                return isXMandatory;
            }
        }
        
        public static string XPrefix {
            get {
                return _xPrefix;
            }
        }
        
        public virtual float X0 {
            get {
                return this.cim_x0.GetValueOrDefault();
            }
            set {
                this.cim_x0 = value;
            }
        }
        
        public virtual bool X0HasValue {
            get {
                return this.cim_x0 != null;
            }
        }
        
        public static bool IsX0Mandatory {
            get {
                return isX0Mandatory;
            }
        }
        
        public static string X0Prefix {
            get {
                return _x0Prefix;
            }
        }
    }
}
