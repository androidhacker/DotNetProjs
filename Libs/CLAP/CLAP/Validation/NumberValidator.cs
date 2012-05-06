﻿using System;

namespace CLAP.Validation
{
    /// <summary>
    /// Number validation
    /// </summary>
    [Serializable]
    public abstract class NumberValidationAttribute : ValidationAttribute
    {
        protected double Number { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        protected NumberValidationAttribute(double number)
        {
            Number = number;
        }
    }

    /// <summary>
    /// Number validation
    /// </summary>
    public abstract class NumberValidator : IValueValidator
    {
        /// <summary>
        /// The number to validate with
        /// </summary>
        public double Number { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        protected NumberValidator(double number)
        {
            Number = number;
        }

        /// <summary>
        /// Validate
        /// </summary>
        public abstract void Validate(ValueInfo info);
    }
}