using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chromophobia
{
    class Sequencer
    {
        static String[] CodonStart = { "ATG" };
        static String[] CodonsStop = { "TAG", "TAA", "TGA" };
        static Dictionary<string, char> GeneticCode = new Dictionary<string, char>
            {
	            {"UUU", 'F'}, {"UUC", 'F'},
                {"UUA", 'L'}, {"UUG", 'L'}, {"CUU", 'L'}, {"CUC", 'L'},{"CUA", 'L'}, {"CUG", 'L'},
                {"AUU", 'I'}, {"AUC", 'I'}, {"AUA", 'I'},
                {"AUG", 'M'},
                {"GUU", 'V'}, {"GUC", 'V'}, {"GUA", 'V'}, {"GUG", 'V'},
                {"UCU", 'S'}, {"UCC", 'S'}, {"UCA", 'S'}, {"UCG", 'S'},
                {"CCU", 'P'}, {"CCC", 'P'}, {"CCA", 'P'}, {"CCG", 'P'},
                {"ACU", 'T'}, {"ACC", 'T'}, {"ACA", 'T'}, {"ACG", 'T'},
                {"GCU", 'A'}, {"GCC", 'A'}, {"GCA", 'A'}, {"GCG", 'A'},
                {"UAU", 'Y'}, {"UAC", 'Y'},
                {"CAU", 'H'}, {"CAC", 'H'},
                {"CAA", 'Q'}, {"CAG", 'Q'},
                {"AAU", 'N'}, {"AAC", 'N'},
                {"AAA", 'K'}, {"AAG", 'K'},
                {"GAU", 'D'}, {"GAC", 'D'},
                {"GAA", 'E'}, {"GAG", 'E'},
                {"UGU", 'C'}, {"UGC", 'C'},
                {"UGG", 'W'}, 
                {"CGU", 'R'}, {"CGC", 'R'}, {"CGA", 'R'}, {"CGG", 'R'},
                {"AGU", 'S'}, {"AGC", 'S'},
                {"AGA", 'R'}, {"AGG", 'R'},
                {"GGU", 'G'}, {"GGC", 'G'}, {"GGA", 'G'}, {"GGG", 'G'},
                {"UAA", '*'}, {"UAG", '*'}, {"UGA", '*'} // Stop
            };

        private ISequenceProvider Provider;

        public Sequencer(ISequenceProvider provider)
        {
            Provider = provider;
        }


        public static String Translate(String sequence, bool ignoreStop=false)
        {
            sequence = sequence.Replace('T', 'U');

            String peptide = "";
            for (int pos = 0; pos < sequence.Length; pos += 3)
            {
                if (sequence.Length <= pos + 2) break;
                String triplet = sequence.Substring(pos, 3);

                char pep = GeneticCode[triplet];
                if (pep == '*') // Stop Codon
                {
                    if (ignoreStop)
                        continue; 
                    else
                        break;
                }
                peptide += pep;
            }
                
            return peptide;
        }

        public static String Transcribe(String sequence)
        {
            String newSeq = "";
            foreach (char c in sequence)
            {
                if      (c == 'A') newSeq += 'U';
                else if (c == 'T') newSeq += 'A';
                else if (c == 'C') newSeq += 'G';
                else if (c == 'G') newSeq += 'C';
                else
                {
                    throw new ArgumentOutOfRangeException("Sequencer::Transcribe() Invalid nucleotide '" + c + "' in sequence");
                }
            }
            return newSeq;
        }

        public static String Complement(String sequence)
        {
            String newSeq = "";
            foreach (char c in sequence)
            {
                if      (c == 'A') newSeq += 'T';
                else if (c == 'T') newSeq += 'A';
                else if (c == 'C') newSeq += 'G';
                else if (c == 'G') newSeq += 'C';
                else
                {
                    throw new ArgumentOutOfRangeException("Sequencer::Complement() Invalid nucleotide '" + c + "' in sequence");
                }
            }
            return newSeq;
        }

        public static String Invert(String sequence)
        {
            String newSeq = "";
            for (int i = sequence.Length-1; i >= 0; i--)
            {
                newSeq += sequence[i];
            }
            return newSeq;
        }

        public static String InvCompl(String sequence)
        {
            return Invert(Complement(sequence));
        }

        public static void InvCompl(ref byte[] buffer)
        {
            /*System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            String s = enc.GetString(buffer);
            s = InvCompl(s);
            buffer = enc.GetBytes(s);*/

            int n = buffer.Length;
            byte[] newBuffer = new byte[n];
            for (int i = 0; i < n; i++)
            {
                // A: 65, T: 84, G: 71, C: 67
                byte b = buffer[i];
                switch (b)
                {
                    case 65: b = 84; break;
                    case 84: b = 65; break;
                    case 71: b = 67; break;
                    case 67: b = 71; break;
                    default: throw new ArgumentOutOfRangeException("Sequencer::InvCompl() Invalid nucleotide '" + b.ToString() + "' in sequence");
                }
                newBuffer[n - i - 1] = b;
            }
            buffer = newBuffer;
        }
/*
        public String Read(int startPosition, int length, int strand = 1, bool downstream = true)
        {
            int dummy;
            return Read(startPosition, length, strand, downstream, out dummy);
        }

        public String Read(int startPosition, int length, int strand, bool downstream, out int newPos)
        {
            int pos = startPosition;
            if (!downstream)
            {
                pos = startPosition-length+1;
            }

            int lengthRead = length;

            String seq = Provider.Read(pos, length);
            if (strand == -1)
            {
                seq = Complement(seq);
            }

            newPos = pos + length;
            if (!downstream)
            {
                newPos = pos-1;
            }
            return seq;
        }*/

        protected static int GetCodonPos(String seq, String[] codons /*, bool downstream=true */)
        {
        	int pos = 0;
        	while ((pos + 3) <= seq.Length)
        	{
                String triplet = seq.Substring(pos, 3);
                for (int j = 0; j < codons.Length; j++)
                {
                    if (triplet == codons[j])
                    {
                        return pos;
                    }
                }
                
                pos += 3;
            }
            return -1;
        }


        public static String GetORF(String sequence, int start=0)
        {
            sequence = sequence.Substring(start);
            
            String seqORF = "";
            
            int posStart = GetCodonPos(sequence, CodonStart);
            if (posStart > -1) 
            {
                String seqEnd = sequence.Substring(posStart);
            	int posEnd = GetCodonPos(seqEnd, CodonsStop);
                if (posEnd == -1) posEnd = seqEnd.Length;
                seqORF = sequence.Substring(posStart, posEnd);
            }

            return seqORF;
        }

    }
}
