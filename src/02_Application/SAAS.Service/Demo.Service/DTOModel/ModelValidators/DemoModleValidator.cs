using Demo.Service.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;


namespace Demo.Service.DtoValidators
{
    public class DemoModleValidator : AbstractValidator<DemoModle>
    {
        public DemoModleValidator()
        {
            //RuleFor(c => c.app_id).GreaterThanOrEqualTo(1).WithMessage("项目不允许为空");
            //RuleFor(c => c.material_type_id).GreaterThanOrEqualTo(1).WithMessage("资源类型不允许为空");
            //RuleFor(c => c.param_1).NotEmpty().WithMessage("参数一不允许为空");
            //RuleFor(c => c.param_2).NotEmpty().WithMessage("参数二不允许为空");
        }
    }
}
