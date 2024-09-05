﻿using Attribute = RoleRollsPocketEdition.Domain.Creatures.Entities.Attribute;

namespace RoleRollsPocketEdition.Application.Creatures.Models
{
    public class AttributeModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public Guid AttributeTemplateId { get; set; }

        public AttributeModel()
        {
        }

        public AttributeModel(Attribute attribute)
        {
            Id = attribute.Id;
            Name = attribute.Name;
            AttributeTemplateId = attribute.AttributeTemplateId;
            Value = attribute.Value;
        }
    }

}