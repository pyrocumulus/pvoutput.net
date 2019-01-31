using System;
using System.Collections.Generic;
using System.Text;

namespace PVOutput.Net.Objects.Status
{
	public interface IStatus
	{
		DateTime Date { get; set; }
		int? EnergyGeneration { get; set; }
		int? PowerGeneration { get; set; }
		int? EnergyConsumption { get; set; }
		int? PowerConsumption { get; set; }
		decimal? NormalisedOutput { get; set; }
		decimal? Temperature { get; set; }
		decimal? Volts { get; set; }
		decimal? ExtendedValue1 { get; set; }
		decimal? ExtendedValue2 { get; set; }
		decimal? ExtendedValue3 { get; set; }
		decimal? ExtendedValue4 { get; set; }
		decimal? ExtendedValue5 { get; set; }
		decimal? ExtendedValue6 { get; set; }
	}
}
