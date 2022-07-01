using System;
using System.Text;
using MatchMaker.Abstract;

namespace MatchMaker.Concrete
{
	public class ASCIICalculator : CalculatorBase, IASCIICalculator
	{
        public override int Calculate(string name)
        {
            return CalculateASCII(name);
        }

        public int CalculateASCII(string name)
        {
            int asciScore = 0;
            byte[] textAsASCII = Encoding.ASCII.GetBytes(name);

            foreach (var item in textAsASCII)
            {
                asciScore += item;
            }
            return asciScore;
        }
		
	}
}

