﻿using RoleRollsPocketEdition._Domain.Creatures.Entities;

namespace RoleRollsPocketEdition._Application.Creatures.Models
{
    public class LifeModel
    {

        public LifeModel()
        {

        }
        public Guid Id { get; set; }
        public int MaxValue { get; set; }
        public int Value { get; set; }
        public string Name { get; set; }
        public Guid LifeTemplateId { get; set; }

        public LifeModel(Life life)
        {
            Id = life.Id;
            LifeTemplateId = life.LifeTemplateId;
            MaxValue = life.MaxValue;
            Value = life.Value;
            Name = life.Name;
        }
    }
}