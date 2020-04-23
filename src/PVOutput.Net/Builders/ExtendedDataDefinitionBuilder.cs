using System;
using System.Collections.Generic;
using System.Text;
using Dawn;
using PVOutput.Net.Enums;
using PVOutput.Net.Objects;
using PVOutput.Net.Objects.Modules.Implementations;

namespace PVOutput.Net.Builders
{
    /// <summary>
    /// Builder for creating extended data definitions, used to update systems.
    /// </summary>
    public sealed class ExtendedDataDefinitionBuilder
    {
        internal ExtendedDataDefinition _definition;

        /// <summary>
        /// Creates a new builder.
        /// </summary>
        public ExtendedDataDefinitionBuilder()
        {
            _definition = new ExtendedDataDefinition();
        }

        /// <summary>
        /// The index that specifies which extended data value to update.
        /// </summary>
        /// <param name="index">The index of the extended data value.</param>
        /// <returns>The builder.</returns>
        public ExtendedDataDefinitionBuilder SetIndex(ExtendedDataIndex index)
        {
            _definition.Index = index;
            return this;
        }

        /// <summary>
        /// Sets the label of the extended data value.
        /// </summary>
        /// <param name="label">The label to set.</param>
        /// <returns>The builder.</returns>
        public ExtendedDataDefinitionBuilder SetLabel(string label)
        {
            Guard.Argument(label).MaxLength(20);

            _definition.Label = label;
            return this;
        }

        /// <summary>
        /// Sets the unit of the extended data value.
        /// </summary>
        /// <param name="unit">The unit to set.</param>
        /// <returns>The builder.</returns>
        public ExtendedDataDefinitionBuilder SetUnit(string unit)
        {
            Guard.Argument(unit).MaxLength(10);

            _definition.Unit = unit;
            return this;
        }

        /// <summary>
        /// Sets the displayed colour of the extended data value.
        /// </summary>
        /// <param name="colour">Hexadecimal colour to display <c>6 hexadecimal characters</c>.</param>
        /// <returns>The builder.</returns>
        public ExtendedDataDefinitionBuilder SetColour(string colour)
        {
            Guard.Argument(colour).Length(6);
            Guard.Argument(colour).Require(IsHexadecimalString, m => "Colour should be a hexadecimal string.");

            _definition.Colour = colour;
            return this;
        }

        /// <summary>
        /// Sets the axis used to display the extended data value on.
        /// </summary>
        /// <param name="axis">The axis to display the extended data value on.</param>
        /// <returns>The builder</returns>
        public ExtendedDataDefinitionBuilder SetAxis(int axis)
        {
            Guard.Argument(axis).InRange(0, 5);

            _definition.Axis = axis;
            return this;
        }

        /// <summary>
        /// Sets the graph type used to display the extended data value.
        /// </summary>
        /// <param name="displayType">Display type to use.</param>
        /// <returns>The builder.</returns>
        public ExtendedDataDefinitionBuilder SetDisplayType(ExtendedDataDisplayType displayType)
        {
            _definition.DisplayType = displayType;
            return this;
        }

        /// <summary>
        /// Uses information within the builder to return the built data definition.
        /// </summary>
        /// <returns>The extended data definition.</returns>
        public IExtendedDataDefinition Build()
        {
            return _definition;
        }

        /// <summary>
        /// Uses information within the builder to return the built data definition.
        /// Resets the builder to it's default state after building.
        /// </summary>
        /// <returns>The extended data definition.</returns>
        public IExtendedDataDefinition BuildAndReset()
        {
            var result = _definition;
            _definition = new ExtendedDataDefinition();
            return result;
        }

        /// <summary>
        /// Resets the builder to it's default state. Ready to build a new definition.
        /// </summary>
        /// <returns>The builder in it's default state.</returns>
        public ExtendedDataDefinitionBuilder Reset()
        {
            _definition = new ExtendedDataDefinition();
            return this;
        }

        private static bool IsHexadecimalString(string colour)
        {
            foreach (char c in colour.ToUpperInvariant())
            {
                bool hexCharacter = (c >= '0' && c <= '9') || (c >= 'A' && c <= 'F');

                if (!hexCharacter)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
