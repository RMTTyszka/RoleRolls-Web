using RoleRollsPocketEdition.CreaturesTemplates.Entities;

namespace RoleRollsPocketEdition.CreaturesTemplates.Dtos
{
    public class LifeTemplateModel
    {
        public LifeTemplateModel()
        {

        }
        public LifeTemplateModel(LifeTemplate life)
        {
            Id = life.Id;
            Name = life.Name;
            Formula = life.Formula;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Formula { get; set; }

    }
}