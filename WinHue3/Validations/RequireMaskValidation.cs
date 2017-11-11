﻿using System;
using System.ComponentModel.DataAnnotations;
using WinHue3.Functions.Schedules.OldCreator;

namespace WinHue3.Validations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class RequireMaskAttribute : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ScheduleCreatorViewModel scmv = validationContext.ObjectInstance as ScheduleCreatorViewModel;
            if (scmv.SelectedType == "W")
            {
                return (byte)value == 0 ? new ValidationResult(GlobalStrings.Schedule_SelectAtLeastOneDay) : ValidationResult.Success;
            }
            else
            {
                return ValidationResult.Success;
            }
        }

    }
}
