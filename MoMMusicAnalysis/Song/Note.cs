using System;
using System.Collections.Generic;
using System.Text;

namespace MoMMusicAnalysis
{
    public class Note<T>
    {
        public int NoteType { get; set; } // TODO Make a FieldNoteType
        public int HitTime { get; set; }
        public T Lane { get; set; }


        public List<byte> RecompileNote()
        {
            throw new NotImplementedException();
        }
    }
}