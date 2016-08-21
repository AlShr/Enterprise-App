using System.ComponentModel.DataAnnotations;

namespace Enterprise.Model
{
    public enum ModuleType
    {
        None,
        [Display(Name = "Module A")]
        ModuleA,
        [Display(Name = "Module B")]
        ModuleB,
    }
}
