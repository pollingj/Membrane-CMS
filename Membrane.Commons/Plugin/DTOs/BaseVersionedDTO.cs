using System;
using Membrane.Commons.FormGeneration.Attributes;
using Membrane.Commons.FormGeneration.Enums;

namespace Membrane.Commons.Plugin.DTOs
{
	public class BaseVersionedDTO : BaseDTO
	{
		[FormFieldType(FieldType.Hidden)]
		public virtual CultureDTO Culture { get; set; }
		[FormFieldType(FieldType.Hidden)]
		public virtual int Revision { get; set; }
		[FormFieldType(FieldType.Hidden)]
		public virtual Guid ParentEntity_Id { get; set; }
		[FormFieldType(FieldType.Hidden)]
		public virtual bool Published { get; set; }
	}
}