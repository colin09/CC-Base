using System;

namespace C.B.Common.helper {
    public class IdGenerator {

        private static UniqueIdGenerator.Net.Generator _generators;

        private static UniqueIdGenerator.Net.Generator Generator () {
            // 0 - 512
            short machineId = (short) 202;
            if (_generators == null)
                _generators = new UniqueIdGenerator.Net.Generator (machineId, DateTime.Today);
            return _generators;
        }

        public static ulong GetLongId () {
            return Generator ().NextLong ();
        }

        public static string GetStringId () {
            return Generator ().Next ();
        }
    }
}