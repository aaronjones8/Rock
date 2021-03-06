//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by the Rock.CodeGeneration project
//     Changes to this file will be lost when the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
// <copyright>
// Copyright by the Spark Development Network
//
// Licensed under the Rock Community License (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.rockrms.com/license
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
//
using System;
using System.Collections.Generic;


namespace Rock.Client
{
    /// <summary>
    /// Base client model for RegistrationTemplateDiscount that only includes the non-virtual fields. Use this for PUT/POSTs
    /// </summary>
    public partial class RegistrationTemplateDiscountEntity
    {
        /// <summary />
        public int Id { get; set; }

        /// <summary />
        public string Code { get; set; }

        /// <summary />
        public decimal DiscountAmount { get; set; }

        /// <summary />
        public decimal DiscountPercentage { get; set; }

        /// <summary />
        public Guid? ForeignGuid { get; set; }

        /// <summary />
        public string ForeignKey { get; set; }

        /// <summary />
        public int Order { get; set; }

        /// <summary />
        public int RegistrationTemplateId { get; set; }

        /// <summary />
        public DateTime? CreatedDateTime { get; set; }

        /// <summary />
        public DateTime? ModifiedDateTime { get; set; }

        /// <summary />
        public int? CreatedByPersonAliasId { get; set; }

        /// <summary />
        public int? ModifiedByPersonAliasId { get; set; }

        /// <summary />
        public Guid Guid { get; set; }

        /// <summary />
        public int? ForeignId { get; set; }

        /// <summary>
        /// Copies the base properties from a source RegistrationTemplateDiscount object
        /// </summary>
        /// <param name="source">The source.</param>
        public void CopyPropertiesFrom( RegistrationTemplateDiscount source )
        {
            this.Id = source.Id;
            this.Code = source.Code;
            this.DiscountAmount = source.DiscountAmount;
            this.DiscountPercentage = source.DiscountPercentage;
            this.ForeignGuid = source.ForeignGuid;
            this.ForeignKey = source.ForeignKey;
            this.Order = source.Order;
            this.RegistrationTemplateId = source.RegistrationTemplateId;
            this.CreatedDateTime = source.CreatedDateTime;
            this.ModifiedDateTime = source.ModifiedDateTime;
            this.CreatedByPersonAliasId = source.CreatedByPersonAliasId;
            this.ModifiedByPersonAliasId = source.ModifiedByPersonAliasId;
            this.Guid = source.Guid;
            this.ForeignId = source.ForeignId;

        }
    }

    /// <summary>
    /// Client model for RegistrationTemplateDiscount that includes all the fields that are available for GETs. Use this for GETs (use RegistrationTemplateDiscountEntity for POST/PUTs)
    /// </summary>
    public partial class RegistrationTemplateDiscount : RegistrationTemplateDiscountEntity
    {
    }
}
