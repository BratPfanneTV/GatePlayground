using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace GATE_PLAYGROUND
{
    class Playground
    {
        public static List<string> displayStrings = new List<string>();
        public static bool xMode = false;
        public static int activeTest = -1;
        public static string levelID = "-1";
        public static string nextLvl = "";
        public static List<List<List<int>>> world = new List<List<List<int>>>();
        public static List<List<List<int>>> lockObj = new List<List<List<int>>>();
        public static string[] LayerNamesTUT = new string[7] {
            "Nothing",
            "Nothing",
            "Nothing",
            "Colors + Keys",
            "Nothing",
            "Nothing",
            "Nothing"
        };
        public static List<string> gates = new List<string>() {
            "     ;     ;     ;     ;     ,", // VOID
            "     ;     ;     ;     ;     ,",
            "     ;     ;     ;     ;     ,",
            "     ;     ;     ;     ;     ,",

            " # # ; ### ; ### ; ### ; # # ,N=S", // straight
            "     ;#####; ### ;#####;     ,E=W",
            " # # ; ### ; ### ; ### ; # # ,S=N",
            "     ;#####; ### ;#####;     ,W=E",

            "     ;#####;##+##;#####; ### ,W + E=S", // AND-GATE
            " ### ;#### ;##+# ;#### ; ### ,N + S=W",
            " ### ;#####;##+##;#####;     ,W + E=N",
            " ### ; ####; #+##; ####; ### ,N + S=E",

            "     ;#####;##/##;#####; ### ,W / E=S", // OR-GATE
            " ### ;#### ;##/# ;#### ; ### ,N / S=W",
            " ### ;#####;##/##;#####;     ,W / E=N",
            " ### ; ####; #/##; ####; ### ,N / S=E",

            "     ;#####;##T##;#####; ### ,S=W+E", // T-GATE
            " ### ;#### ;##T# ;#### ; ### ,W=N+S",
            " ### ;#####;##T##;#####;     ,N=W+E",
            " ### ; ####; #T##; ####; ### ,E=N+S",

            "     ;#### ; ### ;#### ; # # ,W=S", // TURN RIGHT
            " # # ;#### ; ### ;#### ;     ,N=W",
            " # # ; ####; ### ; ####;     ,E=N",
            "     ; ####; ### ; ####; # # ,S=E",

            "     ; ####; ### ; ####; # # ,E=S", // TURN LEFT
            "     ;#### ; ### ;#### ; # # ,S=W",
            " # # ;#### ; ### ;#### ;     ,W=N",
            " # # ; ####; ### ; ####;     ,N=E",

            "     ; ### ; #O# ; ### ; ### ,=S", // SIGNAL EMITTER
            "     ;#### ;##O# ;#### ;     ,=W",
            " ### ; ### ; #O# ; ### ;     ,=N",
            "     ; ####; #O##; ####;     ,=E",

            "     ; ### ; ### ; ### ; ### ,S=C", // LAMPS
            "     ;#### ;#### ;#### ;     ,W=C",
            " ### ; ### ; ### ; ### ;     ,N=C",
            "     ; ####; ####; ####;     ,E=C",

            " ### ; #1# ; #s# ; ### ; ### ,N . A=S", // timing gates 1s
            "     ;#####;##1s#;#####;     ,E . A=W",
            " ### ; ### ; #1# ; #s# ; ### ,S . A=N",
            "     ;#####;#1s##;#####;     ,W . A=E",

            " ### ; #5# ; #s# ; ### ; ### ,N . A=S", // timing gates 5s
            "     ;#####;##5s#;#####;     ,E . A=W",
            " ### ; ### ; #5# ; #s# ; ### ,S . A=N",
            "     ;#####;#5s##;#####;     ,W . A=E",

            " ### ; #3# ; #0# ; #s# ; ### ,N . A=S", // timing gates 30s
            "     ;#####;#30s#;#####;     ,E . A=W",
            " ### ; #3# ; #0# ; #s# ; ### ,S . A=N",
            "     ;#####;#30s#;#####;     ,W . A=E",

            "     ; ### ; #O# ; ### ; ### ,C=S", // BUTTONS
            "     ;#### ;##O# ;#### ;     ,C=W",
            " ### ; ### ; #O# ; ### ;     ,C=N",
            "     ; ####; #O##; ####;     ,C=E",

            " ### ; #1# ; #m# ; ### ; ### ,N . A=S", // timing gates 60s
            "     ;#####;##1m#;#####;     ,E . A=W",
            " ### ; ### ; #1# ; #m# ; ### ,S . A=N",
            "     ;#####;#1m##;#####;     ,W . A=E",

            " ### ; #5# ; #m# ; ### ; ### ,N . A=S", // timing gates 300s
            "     ;#####;##5m#;#####;     ,E . A=W",
            " ### ; ### ; #5# ; #m# ; ### ,S . A=N",
            "     ;#####;#5m##;#####;     ,W . A=E",

            " ### ; ### ; #!# ; ### ; ### ,!N=S", // NOT-GATE
            "     ;#####;##!##;#####;     ,!E=W",
            " ### ; ### ; #!# ; ### ; ### ,!S=N",
            "     ;#####;##!##;#####;     ,!W=E",

            " ### ;#####;##T##;#####; ### ,N=E+S+W", // TRISPLIT
            " ### ;#####;##T##;#####; ### ,E=S+W+N",
            " ### ;#####;##T##;#####; ### ,S=W+N+E",
            " ### ;#####;##T##;#####; ### ,W=N+E+S",

            " ### ; ####; #/##; ####; ### ,E / N=S", // OR-GATE
            "     ;#####;##/##;#####; ### ,S / E=W",
            " ### ;#### ;##/# ;#### ; ### ,W / S=N",
            " ### ;#####;##/##;#####;     ,N / W=E",

            " ### ; ####; #/##; ####; ### ,S / E=N", // OR-GATE
            "     ;#####;##/##;#####; ### ,W / S=E",
            " ### ;#### ;##/# ;#### ; ### ,N / W=S",
            " ### ;#####;##/##;#####;     ,E / N=W",

            " ### ; ### ;##^##; ### ;  #  ,N=T", // UP
            "  #  ; ####;##^##; ####;  #  ,E=T",
            "  #  ; ### ;##^##; ### ; ### ,S=T",
            "  #  ;#### ;##^##;#### ;  #  ,W=T",

            " ### ; ### ;##V##; ### ;  #  ,N=B", // UP
            "  #  ; ####;##V##; ####;  #  ,E=B",
            "  #  ; ### ;##V##; ### ; ### ,S=B",
            "  #  ;#### ;##V##;#### ;  #  ,W=B",

            "     ; ### ; #^# ; ### ;     ,B=T",
            "     ; ### ; #V# ; ### ;     ,T=B",
            "     ; ### ; #^# ; ### ;     ,B=T",
            "     ; ### ; #V# ; ### ;     ,T=B",

            " ### ; ### ;##V##; ### ;  #  ,T=N", // UP
            "  #  ; ####;##V##; ####;  #  ,T=E",
            "  #  ; ### ;##V##; ### ; ### ,T=S",
            "  #  ;#### ;##V##;#### ;  #  ,T=W",

            " ### ; ### ;##^##; ### ;  #  ,B=N", // UP
            "  #  ; ####;##^##; ####;  #  ,B=E",
            "  #  ; ### ;##^##; ### ; ### ,B=S",
            "  #  ;#### ;##^##;#### ;  #  ,B=W",

            " ### ; #1# ; #0# ; #m# ; ### ,N . A=S", // timing gates 600s
            "     ;#####;#10m#;#####;     ,E . A=W",
            " ### ; #1# ; #0# ; #m# ; ### ,S . A=N",
            "     ;#####;#10m#;#####;     ,W . A=E",

            " ### ; #3# ; #0# ; #m# ; ### ,N . A=S", // timing gates 1800s
            "     ;#####;#30m#;#####;     ,E . A=W",
            " ### ; #3# ; #0# ; #m# ; ### ,S . A=N",
            "     ;#####;#30m#;#####;     ,W . A=E",

            " ### ; #1# ; #h# ; ### ; ### ,N . A=S", // timing gates 3600s
            "     ;#####;##1h#;#####;     ,E . A=W",
            " ### ; ### ; #1# ; #h# ; ### ,S . A=N",
            "     ;#####;#1h##;#####;     ,W . A=E",

            "Press SPACE to start.        ;" +
            "Press P to enter the         ;" +
            "Playground.                  ;" +
            "Press T for a Tutorial.      ;" +
            "                             ,",
            ";;;;,",
            "     ;     ;;     ;     ,",
            " ;;;;,",

            "Welcome to the Playground.   ;" +
            "HereCOMMA you can do what you    ;" +
            "wantCOMMA and all objects are un-;" +
            "locked. Have fun!            ;" +
            "                             ,",
            ";;;;,",
            ";;;;,",
            ";;;;,",

            "     ;  K  ; #1# ;  #  ;     ,A Fkey1 A=S",
            "     ;  K  ; #1# ;  #  ;     ,A Fkey1 A=W",
            "     ;  K  ; #1# ;  #  ;     ,A Fkey1 A=N",
            "     ;  K  ; #1# ;  #  ;     ,A Fkey1 A=E",

            "     ;  K  ; #2# ;  #  ;     ,A Fkey2 A=S",
            "     ;  K  ; #2# ;  #  ;     ,A Fkey2 A=W",
            "     ;  K  ; #2# ;  #  ;     ,A Fkey2 A=N",
            "     ;  K  ; #2# ;  #  ;     ,A Fkey2 A=E",

            "     ;  K  ; #3# ;  #  ;     ,A Fkey3 A=S",
            "     ;  K  ; #3# ;  #  ;     ,A Fkey3 A=W",
            "     ;  K  ; #3# ;  #  ;     ,A Fkey3 A=N",
            "     ;  K  ; #3# ;  #  ;     ,A Fkey3 A=E",

            "     ;  K  ; #4# ;  #  ;     ,A Fkey4 A=S",
            "     ;  K  ; #4# ;  #  ;     ,A Fkey4 A=W",
            "     ;  K  ; #4# ;  #  ;     ,A Fkey4 A=N",
            "     ;  K  ; #4# ;  #  ;     ,A Fkey4 A=E",

            "     ;  K  ; #5# ;  #  ;     ,A Fkey5 A=S",
            "     ;  K  ; #5# ;  #  ;     ,A Fkey5 A=W",
            "     ;  K  ; #5# ;  #  ;     ,A Fkey5 A=N",
            "     ;  K  ; #5# ;  #  ;     ,A Fkey5 A=E",

            "     ;  K  ; #6# ;  #  ;     ,A Fkey6 A=S",
            "     ;  K  ; #6# ;  #  ;     ,A Fkey6 A=W",
            "     ;  K  ; #6# ;  #  ;     ,A Fkey6 A=N",
            "     ;  K  ; #6# ;  #  ;     ,A Fkey6 A=E",

            "     ;  K  ; #7# ;  #  ;     ,A Fkey7 A=S",
            "     ;  K  ; #7# ;  #  ;     ,A Fkey7 A=W",
            "     ;  K  ; #7# ;  #  ;     ,A Fkey7 A=N",
            "     ;  K  ; #7# ;  #  ;     ,A Fkey7 A=E",

            "     ;  K  ; #8# ;  #  ;     ,A Fkey8 A=S",
            "     ;  K  ; #8# ;  #  ;     ,A Fkey8 A=W",
            "     ;  K  ; #8# ;  #  ;     ,A Fkey8 A=N",
            "     ;  K  ; #8# ;  #  ;     ,A Fkey8 A=E",

            "     ;#####;##+##;#####; ### ,W + S=E", // AND-GATE
            " ### ;#### ;##+# ;#### ; ### ,N + W=S",
            " ### ;#####;##+##;#####;     ,E + N=W",
            " ### ; ####; #+##; ####; ### ,S + E=N",

            "     ;#####;##+##;#####; ### ,E + S=W", // AND-GATE
            " ### ;#### ;##+# ;#### ; ### ,S + W=N",
            " ### ;#####;##+##;#####;     ,W + N=E",
            " ### ; ####; #+##; ####; ### ,N + E=S",

            "     ;#####;##T##;#####; ### ,E=W+S", // SPLIT
            " ### ;#### ;##T# ;#### ; ### ,S=N+W",
            " ### ;#####;##T##;#####;     ,W=E+N",
            " ### ; ####; #T##; ####; ### ,N=S+E",

            "     ;#####;##T##;#####; ### ,W=E+S", // SPLIT
            " ### ;#### ;##T# ;#### ; ### ,N=W+S",
            " ### ;#####;##T##;#####;     ,E=W+N",
            " ### ; ####; #T##; ####; ### ,S=N+E",

            "     ;  ^  ; #B# ;  #  ;     ,S=ABeep", // BEEP
            "     ;  ^  ; #B# ;  #  ;     ,W=ABeep",
            "     ;  ^  ; #B# ;  #  ;     ,N=ABeep",
            "     ;  ^  ; #B# ;  #  ;     ,E=ABeep",

            "     ;     ;     ;     ;START,",
            "     ;     ;     ;PLAY ;GROUND,",
            "     ;     ;     ;     ;     ,",
            "     ;     ;     ;     ;     ,",

            "     ;  #  ; #\u263a# ;  #  ;     ,W=ALoWoPlayground",
            "     ;  #  ; #\u263a# ;  #  ;     ,W=ALoWostring0",
            ";;;;,",
            ";;;;,",
            
            // DATA CABLES
            " ### ; #~# ; #~# ; #~# ; ### ,N D| A=S", // straight
            "     ;#####;#~~~#;#####;     ,E D| A=W",
            " ### ; #~# ; #~# ; #~# ; ### ,S D| A=N",
            "     ;#####;#~~~#;#####;     ,W D| A=E",

            "     ;#####;#~%~#;##~##; ### ,W D& E=S", // AND-GATE
            " ### ;##~# ;#~%# ;##~# ; ### ,N D& S=W",
            " ### ;##~##;#~%~#;#####;     ,W D& E=N",
            " ### ; #~##; #%~#; #~##; ### ,N D& S=E",

            "     ;#####;#~/~#;##~##; ### ,W D| E=S", // OR-GATE
            " ### ;##~# ;#~/# ;##~# ; ### ,N D| S=W",
            " ### ;##~##;#~/~#;#####;     ,W D| E=N",
            " ### ; #~##; #/~#; #~##; ### ,N D| S=E",

            "     ;#####;#~+~#;##~##; ### ,W D+ E=S", // Addition-GATE
            " ### ;##~# ;#~+# ;##~# ; ### ,N D+ S=W",
            " ### ;##~##;#~+~#;#####;     ,W D+ E=N",
            " ### ; #~##; #+~#; #~##; ### ,N D+ S=E",

            "     ;#####;#~T~#;##~##; ### ,S D| A=W+E", // SplIT-GATE
            " ### ;##~# ;#~T# ;##~# ; ### ,W D| A=N+S",
            " ### ;##~##;#~T~#;#####;     ,N D| A=W+E",
            " ### ; #~##; #T~#; #~##; ### ,E D| A=N+S",

            "     ;#### ; ~~# ;##~# ; # # ,W D| A=S", // TURN RIGHT
            " # # ;##~# ; ~~# ;#### ;     ,N D| A=W",
            " # # ; #~##; #~~ ; ####;     ,E D| A=N",
            "     ; ####; #~~ ; #~##; # # ,S D| A=E",

            "     ; ####; #~~ ; #~##; #~# ,E D| A=S", // TURN LEFT
            "     ;#### ; ~~# ;##~# ; #~# ,S D| A=W",
            " # # ;##~# ; ~~# ;#### ;     ,W D| A=N",
            " # # ; #~##; #~~ ; ####;     ,N D| A=E",

            "     ; ### ; #O# ; #~# ; ### ,A Dv A=S", // DATA EMITTER
            "     ;#### ;#~O# ;#### ;     ,A Dv A=W",
            " ### ; #~# ; #O# ; ### ;     ,A Dv A=N",
            "     ; ####; #O~#; ####;     ,A Dv A=E",

            "     ; ### ; #o# ; #~# ; ### ,A Dd A=S", // DATA EMITTER
            "     ;#### ;#~o# ;#### ;     ,A Dd A=W",
            " ### ; #~# ; #o# ; ### ;     ,A Dd A=N",
            "     ; ####; #o~#; ####;     ,A Dd A=E",

            "#####;#   #;#  0#;#   #;#####,N D| A=S+AShDa",
            "#####;#   #;#  0#;#   #;#####,E D| A=W+AShDa",
            "#####;#   #;#  0#;#   #;#####,S D| A=N+AShDa",
            "#####;#   #;#  0#;#   #;#####,W D| A=E+AShDa",

            "     ;  #  ; #K# ;  #  ;     ,A Fkeyr A=S",
            "     ;  #  ; #K# ;  #  ;     ,A Fkeyr A=W",
            "     ;  #  ; #K# ;  #  ;     ,A Fkeyr A=N",
            "     ;  #  ; #K# ;  #  ;     ,A Fkeyr A=E",

            "     ;#####;#~=~#;##~##; ### ,W DC E=S", // CHECK-GATE
            " ### ;##~# ;#~=# ;##~# ; ### ,N DC S=W",
            " ### ;##~##;#~=~#;#####;     ,W DC E=N",
            " ### ; #~##; #=~#; #~##; ### ,N DC S=E",

            "     ; ### ; #1# ; #~# ; ### ,=ADaEm1+S", // DATA EMITTER
            "     ;#### ;#~1# ;#### ;     ,=ADaEm1+W",
            " ### ; #~# ; #1# ; ### ;     ,=ADaEm1+N",
            "     ; ####; #1~#; ####;     ,=ADaEm1+E",

            "     ; ### ; #2# ; #~# ; ### ,=ADaEm2+S", // DATA EMITTER
            "     ;#### ;#~2# ;#### ;     ,=ADaEm2+W",
            " ### ; #~# ; #2# ; ### ;     ,=ADaEm2+N",
            "     ; ####; #2~#; ####;     ,=ADaEm2+E",

            "     ; ### ; #4# ; #~# ; ### ,=ADaEm4+S", // DATA EMITTER
            "     ;#### ;#~4# ;#### ;     ,=ADaEm4+W",
            " ### ; #~# ; #4# ; ### ;     ,=ADaEm4+N",
            "     ; ####; #4~#; ####;     ,=ADaEm4+E",

            "     ; ### ; #8# ; #~# ; ### ,=ADaEm8+S", // DATA EMITTER
            "     ;#### ;#~8# ;#### ;     ,=ADaEm8+W",
            " ### ; #~# ; #8# ; ### ;     ,=ADaEm8+N",
            "     ; ####; #8~#; ####;     ,=ADaEm8+E",

            "     ; #1# ; #6# ; #~# ; ### ,=ADaEm16+S", // DATA EMITTER
            "     ;#### ;#~16 ;#### ;     ,=ADaEm16+W",
            " ### ; #~# ; #1# ; #6# ;     ,=ADaEm16+N",
            "     ; ####; 16~#; ####;     ,=ADaEm16+E",

            "     ; #3# ; #2# ; #~# ; ### ,=ADaEm32+S", // DATA EMITTER
            "     ;#### ;#~32 ;#### ;     ,=ADaEm32+W",
            " ### ; #~# ; #3# ; #2# ;     ,=ADaEm32+N",
            "     ; ####; 32~#; ####;     ,=ADaEm32+E",

            "     ; #6# ; #4# ; #~# ; ### ,=ADaEm64+S", // DATA EMITTER
            "     ;#### ;#~64 ;#### ;     ,=ADaEm64+W",
            " ### ; #~# ; #6# ; #4# ;     ,=ADaEm64+N",
            "     ; ####; 64~#; ####;     ,=ADaEm64+E",

            "     ; ### ; 128 ; #~# ; ### ,=ADaEm128+S", // DATA EMITTER
            "     ;#### ;#128 ;#### ;     ,=ADaEm128+W",
            " ### ; #~# ; 128 ; ### ;     ,=ADaEm128+N",
            "     ; ####; 128#; ####;     ,=ADaEm128+E",

            "     ; ### ; 256 ; #~# ; ### ,=ADaEm256+S", // DATA EMITTER
            "     ;#### ;#256 ;#### ;     ,=ADaEm256+W",
            " ### ; #~# ; 256 ; ### ;     ,=ADaEm256+N",
            "     ; ####; 256#; ####;     ,=ADaEm256+E",

            "     ; ### ; 512 ; #~# ; ### ,=ADaEm512+S", // DATA EMITTER
            "     ;#### ;#512 ;#### ;     ,=ADaEm512+W",
            " ### ; #~# ; 512 ; ### ;     ,=ADaEm512+N",
            "     ; ####; 512#; ####;     ,=ADaEm512+E",
            // END OF DATA CABLES

            " #~# ; #~# ;##^##; #~# ;  #  ,N D| A=T", // UP
            "  #  ; ####;#~^~~; ####;  #  ,E D| A=T",
            "  #  ; #~# ;##^##; #~# ; #~# ,S D| A=T",
            "  #  ;#### ;~~^~#;#### ;  #  ,W D| A=T",

            " #~# ; #~# ;##V##; #~# ;  #  ,N D| A=B", // UP
            "  #  ; ####;#~V~~; ####;  #  ,E D| A=B",
            "  #  ; #~# ;##V##; #~# ; #~# ,S D| A=B",
            "  #  ;#### ;~~V~#;#### ;  #  ,W D| A=B",

            "     ; ### ; ~^~ ; ### ;     ,B D| A=T",
            "     ; ### ; ~V~ ; ### ;     ,T D| A=B",
            "     ; ### ; ~^~ ; ### ;     ,B D| A=T",
            "     ; ### ; ~V~ ; ### ;     ,T D| A=B",

            " #~# ; #~# ;##V##; #~# ;  #  ,T D| A=N", // UP
            "  #  ; ####;#~V~~; ####;  #  ,T D| A=E",
            "  #  ; #~# ;##V##; #~# ; #~# ,T D| A=S",
            "  #  ;#### ;~~V~#;#### ;  #  ,T D| A=W",

            " #~# ; #~# ;##^##; #~# ;  #  ,B D| A=N", // UP
            "  #  ; ####;#~^~~; ####;  #  ,B D| A=E",
            "  #  ; #~# ;##^##; #~# ; #~# ,B D| A=S",
            "  #  ;#### ;~~^~#;#### ;  #  ,B D| A=W",

            "     ; 1#2 ; 0#4 ; #~# ; ### ,=ADaEm1024+S", // DATA EMITTER
            "     ;1024 ;#### ;#### ;     ,=ADaEm1024+W",
            " ### ; #~# ; 1#2 ; 0#4 ;     ,=ADaEm1024+N",
            "     ; 1024; ####; ####;     ,=ADaEm1024+E",

            "     ;#####;#~%~#;##~##; ### ,W D& S=E", // AND-GATE
            " ### ;##~# ;#~%# ;##~# ; ### ,W D& S=N",
            " ### ;##~##;#~%~#;#####;     ,N D& E=W",
            " ### ; #~##; #%~#; #~##; ### ,N D& E=S",

            "     ;#####;#~/~#;##~##; ### ,W D| S=E", // OR-GATE
            " ### ;##~# ;#~/# ;##~# ; ### ,W D| S=N",
            " ### ;##~##;#~/~#;#####;     ,N D| E=W",
            " ### ; #~##; #/~#; #~##; ### ,N D| E=S",

            "     ;#####;#~+~#;##~##; ### ,W D+ S=E", // Addition-GATE
            " ### ;##~# ;#~+# ;##~# ; ### ,W D+ S=N",
            " ### ;##~##;#~+~#;#####;     ,N D+ E=W",
            " ### ; #~##; #+~#; #~##; ### ,N D+ E=S",

            "     ;#####;#~T~#;##~##; ### ,E D| A=W+S", // SplIT-GATE
            " ### ;##~# ;#~T# ;##~# ; ### ,N D| A=W+S",
            " ### ;##~##;#~T~#;#####;     ,W D| A=N+E",
            " ### ; #~##; #T~#; #~##; ### ,S D| A=N+E",

            "     ;#####;#~%~#;##~##; ### ,S D& E=W", // AND-GATE
            " ### ;##~# ;#~%# ;##~# ; ### ,N D& W=S",
            " ### ;##~##;#~%~#;#####;     ,W D& N=E",
            " ### ; #~##; #%~#; #~##; ### ,E D& S=N",

            "     ;#####;#~/~#;##~##; ### ,S D| E=W", // OR-GATE
            " ### ;##~# ;#~/# ;##~# ; ### ,N D| W=S",
            " ### ;##~##;#~/~#;#####;     ,W D| N=E",
            " ### ; #~##; #/~#; #~##; ### ,E D| S=N",

            "     ;#####;#~+~#;##~##; ### ,S D+ E=W", // Addition-GATE
            " ### ;##~# ;#~+# ;##~# ; ### ,N D+ W=S",
            " ### ;##~##;#~+~#;#####;     ,W D+ N=E",
            " ### ; #~##; #+~#; #~##; ### ,E D+ S=N",

            "     ;#####;#~T~#;##~##; ### ,W D| A=S+E", // SplIT-GATE
            " ### ;##~# ;#~T# ;##~# ; ### ,S D| A=N+W",
            " ### ;##~##;#~T~#;#####;     ,E D| A=W+N",
            " ### ; #~##; #T~#; #~##; ### ,N D| A=E+S",

            "     ;#####;#~=~#;##~##; ### ,W DC S=E", // CHECK-GATE
            " ### ;##~# ;#~=# ;##~# ; ### ,W DC S=N",
            " ### ;##~##;#~=~#;#####;     ,N DC E=W",
            " ### ; #~##; #=~#; #~##; ### ,N DC E=S",

            "     ;#####;#~=~#;##~##; ### ,S DC E=W", // CHECK-GATE
            " ### ;##~# ;#~=# ;##~# ; ### ,N DC W=S",
            " ### ;##~##;#~=~#;#####;     ,W DC N=E",
            " ### ; #~##; #=~#; #~##; ### ,E DC S=N",

            "#####;#####;#####;#####;#####,B Fdclear B=ADisp",
            "#####;#####;#####;#####;#####,B Fdclear B=ADisp",
            "#####;#####;#####;#####;#####,B Fdclear B=ADisp",
            "#####;#####;#####;#####;#####,B Fdclear B=ADisp",

            "     ;#####;#~\u2566~#;##~##; ### ,S Dk E=W", // FLOOD-GATE
            " ### ;##~# ;#~\u2563# ;##~# ; ### ,W Dk N=S",
            " ### ;##~##;#~\u2569~#;#####;     ,N Dk W=E",
            " ### ; #~##; #\u2560~#; #~##; ### ,E Dk S=N",

            "     ;     ;     ;     ;#####,", // VISUAL BORDERS
            "#    ;#    ;#    ;#    ;#    ,",
            "#####;     ;     ;     ;     ,",
            "    #;    #;    #;    #;    #,",

            "     ;     ;     ;     ;    #,", // VISUAL BORDER CORNERS
            "     ;     ;     ;     ;#    ,",
            "    #;     ;     ;     ;     ,",
            "#    ;     ;     ;     ;     ,",

            " ### ; ### ; ### ; ### ;#####,C levelin N=S",
            " ### ; ### ; ### ; ### ;#####,C levelin N=S",
            "#####; ### ; ### ; ### ; ### ,C levelin N=S",
            "#####; ### ; ### ; ### ; ### ,C levelin N=S",

            " ### ;##~##;#~T~#;##~##; ### ,N D| A=E+S+W", // TRISPLIT
            " ### ;##~##;#~T~#;##~##; ### ,E D| A=S+W+N",
            " ### ;##~##;#~T~#;##~##; ### ,S D| A=W+N+E",
            " ### ;##~##;#~T~#;##~##; ### ,W D| A=N+E+S",

            "     ;     ;  = Signal Input ;     ;     ,W=A",
            "     ;     ;  = Signal Output;     ;     ,A=W",
            "     ;     ;  = Data Input   ;     ;     ,W=A",
            "     ;     ;  = Data Output  ;     ;     ,A=W",

            "     ;     ;  = Active       ;     ;     ,=W",
            "Controls:;Arrow Keys: Move Cursor;W/S: Switch layer;A: Open Module Selector;  Q/E: Change variant;Enter: Select/Place/Remove;Space: Send signal to button;C/Y: Rotate object;X: Enter Verification Mode,",
            "     ;     ;  = Active       ;     ;     ,=W",
            "     ;     ;  = Active       ;     ;     ,=W",

            " ;  Layer 3: " + LayerNamesTUT[0] + ";" +
            "  Layer 2: " + LayerNamesTUT[1] + ";" +
            "  Layer 1: " + LayerNamesTUT[2] + ";" +
            "  Layer 0: " + LayerNamesTUT[3] + ",", // 360

            " ;;  Layer 3: " + LayerNamesTUT[0] + ";" +
            "  Layer 2: " + LayerNamesTUT[1] + ";" +
            "  Layer 1: " + LayerNamesTUT[2] + ",", // 361

            " ;;;  Layer 3: " + LayerNamesTUT[0] + ";" +
            "  Layer 2: " + LayerNamesTUT[1] + ",", // 362

            " ;;;;  Layer 3: " + LayerNamesTUT[0] + ",", // 363

            " ;;  Layer 3: " + LayerNamesTUT[0] + ";" +
            "  Layer 2: " + LayerNamesTUT[1] + ";" +
            "  Layer 1: " + LayerNamesTUT[2] + ";" +
            "  Layer 0: " + LayerNamesTUT[3] + ";" +
            "  Layer -1: " + LayerNamesTUT[4] + ";" +
            "  Layer -2: " + LayerNamesTUT[5] + ";" +
            "  Layer -3: " + LayerNamesTUT[6] + ",=W", // 364

            " ;  Layer 3: " + LayerNamesTUT[0] + ";" +
            "  Layer 2: " + LayerNamesTUT[1] + ";" +
            "  Layer 1: " + LayerNamesTUT[2] + ";" +
            "  Layer 0: " + LayerNamesTUT[3] + ";" +
            "  Layer -1: " + LayerNamesTUT[4] + ";" +
            "  Layer -2: " + LayerNamesTUT[5] + ";" +
            "  Layer -3: " + LayerNamesTUT[6] + ",=W", // 365

            "  Layer 3: " + LayerNamesTUT[0] + ";" +
            "  Layer 2: " + LayerNamesTUT[1] + ";" +
            "  Layer 1: " + LayerNamesTUT[2] + ";" +
            "  Layer 0: " + LayerNamesTUT[3] + ";" +
            "  Layer -1: " + LayerNamesTUT[4] + ";" +
            "  Layer -2: " + LayerNamesTUT[5] + ";" +
            "  Layer -3: " + LayerNamesTUT[6] + ",=W", // 366
            
            "  Layer 2: " + LayerNamesTUT[1] + ";" +
            "  Layer 1: " + LayerNamesTUT[2] + ";" +
            "  Layer 0: " + LayerNamesTUT[3] + ";" +
            "  Layer -1: " + LayerNamesTUT[4] + ";" +
            "  Layer -2: " + LayerNamesTUT[5] + ";" +
            "  Layer -3: " + LayerNamesTUT[6] + ",=W", // 367

            "  Layer 1: " + LayerNamesTUT[2] + ";" +
            "  Layer 0: " + LayerNamesTUT[3] + ";" +
            "  Layer -1: " + LayerNamesTUT[4] + ";" +
            "  Layer -2: " + LayerNamesTUT[5] + ";" +
            "  Layer -3: " + LayerNamesTUT[6] + ",=W", // 368
            
            "  Layer 0: " + LayerNamesTUT[3] + ";" +
            "  Layer -1: " + LayerNamesTUT[4] + ";" +
            "  Layer -2: " + LayerNamesTUT[5] + ";" +
            "  Layer -3: " + LayerNamesTUT[6] + ",=W", // 369

            "  Layer -1: " + LayerNamesTUT[4] + ";" +
            "  Layer -2: " + LayerNamesTUT[5] + ";" +
            "  Layer -3: " + LayerNamesTUT[6] + ",=W", // 370
            ",", // 371
        }; // Press 'D' followed by '2' and enter 'LEVEL0' to start. Enter the next ID after every level.

        public static Dictionary<int, int> timingGates = new Dictionary<int, int>() {
            {36, 1},
            {37, 1},
            {38, 1},
            {39, 1},
            {40, 5},
            {41, 5},
            {42, 5},
            {43, 5},
            {44, 30},
            {45, 30},
            {46, 30},
            {47, 30},
            {48, 60},
            {49, 60},
            {50, 60},
            {51, 60},
            {52, 300},
            {53, 300},
            {54, 300},
            {55, 300},
            {96, 600},
            {97, 600},
            {98, 600},
            {99, 600},
            {100, 1800},
            {101, 1800},
            {102, 1800},
            {103, 1800},
            {104, 3600},
            {105, 3600},
            {106, 3600},
            {107, 3600},
        };
        public static List<int> dataModules = new List<int>() {
            176,
            177,
            178,
            179,
            180,
            181,
            182,
            183,
            184,
            185,
            186,
            187,
            188,
            189,
            190,
            191,
            192,
            193,
            194,
            195,
            196,
            197,
            198,
            199,
            200,
            201,
            202,
            203,
            204,
            205,
            206,
            207,
            208,
            209,
            210,
            211,
            212,
            213,
            214,
            215,
            216,
            217,
            218,
            219,
            220,
            221,
            222,
            223,
            224,
            225,
            226,
            227,
            228,
            229,
            230,
            231,
            232,
            233,
            234,
            235,
            236,
            237,
            238,
            239,
            240,
            241,
            242,
            243,
            244,
            245,
            246,
            247,
            248,
            249,
            250,
            251,
            252,
            253,
            254,
            255,
            256,
            257,
            258,
            259,
            260,
            261,
            262,
            263,
            264,
            265,
            266,
            267,
            268,
            269,
            270,
            271,
            272,
            273,
            274,
            275,
            276,
            277,
            278,
            279,
            280,
            281,
            282,
            283,
            284,
            285,
            286,
            287,
            288,
            289,
            290,
            291,
            292,
            293,
            294,
            295,
            296,
            297,
            298,
            299,
            300,
            301,
            302,
            303,
            304,
            305,
            306,
            307,
            308,
            309,
            310,
            311,
            312,
            313,
            314,
            315,
            316,
            317,
            318,
            319,
            320,
            321,
            322,
            323,
            324,
            325,
            326,
            327,
            332,
            333,
            334,
            335,
            348,
            349,
            350,
            351,
            354,
            355
        };
        public static List<string> noDataSides = new List<string>()
        {
            "332-S",
            "333-W",
            "334-N",
            "335-E",
        };
        public static Dictionary<string, Dictionary<string, int>> selector = new Dictionary<string, Dictionary<string, int>>() {
            {"PIPES", new Dictionary<string, int>() {
                { "STRAIGHT", 7 },
                { " TURN-01", 26 },
                { " TURN-02", 20 },
                { "VERTICAL-01", 84 },
                { "VERTICAL-02", 85 },
            } },
            {"I/O", new Dictionary<string, int>()
            {
                { " BUTTON", 51 },
                { "EMITTER", 31 },
                { " LAMP    K", 33 },
                { "EYREADER", 216 },
                { " BEEPER", 164 },
                { "DISPLAY SEGMENT", 328 }
            } },
            { "LOGIC", new Dictionary<string, int>()
            {
                { "  AND-01", 8 },
                { "  AND-02", 148 },
                { "  AND-03", 152 },
                { "  OR-01", 12 },
                { "  OR-02", 73 },
                { "  OR-03", 69 },
                { " SPLIT-01", 16 },
                { " SPLIT-02", 156 },
                { " SPLIT-03", 160 },
                { " SPLIT-04", 66 },
                { "  NOT", 63 }
            } },
            { "TIMING", new Dictionary<string, int>()
            {
                {" SHORT-01", 39 },
                {" SHORT-02", 43 },
                {" SHORT-03", 47 },
                {" MIDDLE-01", 55 },
                {" MIDDLE-02", 59 },
                {" MIDDLE-03", 99},
                {" MIDDLE-04", 103},
                {" LONG-01", 107}
            } },
            { "LAYER MANAGEMENT", new Dictionary<string, int>()
            {
                { "  IN-01", 79},
                { "  IN-02", 83},
                { "  OUT    V-01", 89},
                { "  OUT    V-02", 93},
                { "ERT. PIPE-01", 84 },
                { "ERT. PIPE-02", 85 },
                { " DATA IN-01", 267},
                { " DATA IN-02", 271},
                { "DATA OUT V-01", 277},
                { "DATA OUT V-02", 281},
                { "ERT. dPIPE-01", 272 },
                { "ERT. dPIPE-02", 273 }
            } },
            { "DATA TRANSMISSION", new Dictionary<string, int>()
            {
                { " dPIPE  dP", 179 },
                { "IPE TURN  -01", 202 },
                { "IPE TURN  -02", 196 },
                { "DISPLAY", 215 },
                { "AND-01",  180 },
                { "AND-02",  288 },
                { "AND-03",  304 },
                { "OR-01",  184 },
                { "OR-02",  292 },
                { "OR-03",  308 },
                { "ADDITION-01",  188 },
                { "ADDITION-02",  296 },
                { "ADDITION-03",  312 },
                { "SPLIT-01",  192 },
                { "SPLIT-02",  300 },
                { "SPLIT-03",  316 },
                { "SPLIT-04",  348 },
                { "COMPARE-01", 220 },
                { "COMPARE-02", 320 },
                { "COMPARE-03", 324 },
                { "EMITTER-01", 224 },
                { "EMITTER-02", 228 },
                { "EMITTER-03", 232 },
                { "EMITTER-04", 236 },
                { "EMITTER-05", 240 },
                { "EMITTER-06", 244 },
                { "EMITTER-07", 248 },
                { "EMITTER-08", 252 },
                { "EMITTER-09", 256 },
                { "EMITTER-10", 260 },
                { "EMITTER-11", 284 },
                { "FLOODGATE",  332 },
            } }
        };
        public static List<List<List<List<int>>>> signals = new List<List<List<List<int>>>>();
        public static List<List<List<List<int>>>> signalCopy = new List<List<List<List<int>>>>();
        public static List<List<List<List<int>>>> signalAge = new List<List<List<List<int>>>>();
        public static List<List<List<List<int>>>> signalHold = new List<List<List<List<int>>>>();
        public static List<List<List<List<int>>>> signalHoldCopy = new List<List<List<List<int>>>>();
        public static List<List<List<List<int>>>> signalHoldAge = new List<List<List<List<int>>>>();
        public static List<List<List<List<DateTime>>>> signalHoldTimestamp = new List<List<List<List<DateTime>>>>();
        public static int layers = 7;
        public static int height = 10;
        public static int width = 20;
        public static int Spacing = 0;
        public static int LoadedLayer = layers / 2;
        public static int baseLayer = layers / 2;
        public static double TicksPerSecond = 100;
        public static List<int> neighbourSignals = new List<int>() {
            5,
            3,
            4,
            1,
            2,
            0,
            6
        };
        public static bool resetWorld = false;
        public static List<List<int>> neighbours = new List<List<int>>()
        {
            new List<int>()
            {
                1,
                0,
                0
            },
            new List<int>()
            {
                0,
                0,
                -1
            },
            new List<int>()
            {
                0,
                1,
                0
            },
            new List<int>()
            {
                0,
                0,
                1
            },
            new List<int>()
            {
                0,
                -1,
                0
            },
            new List<int>()
            {
                -1,
                0,
                0
            },
            new List<int>()
            {
                0,
                0,
                0
            }
        };
        public static Dictionary<string, List<int>> sideDisplays = new Dictionary<string, List<int>>()
        {
            {"T", new List<int>()
            {
                2,
                1
            }},
            {"B", new List<int>()
            {
                2,
                3
            }},
            {"N", new List<int>()
            {
                2,
                0
            }},
            {"S", new List<int>()
            {
                2,
                4
            }},
            {"E", new List<int>()
            {
                4,
                2
            }},
            {"W", new List<int>()
            {
                0,
                2
            }},
            {"C", new List<int>()
            {
                2,
                2
            }}

        };
        public static Dictionary<string, List<int>> debugDisplays = new Dictionary<string, List<int>>()
        {
            { "T", new List<int>()
            {
                2,
                -1
            } },
            { "B", new List<int>()
            {
                2,
                5
            } },
            { "N", new List<int>()
            {
                0,
                -1
            } },
            { "S", new List<int>()
            {
                4,
                5
            } },
            { "E", new List<int>()
            {
                5,
                0
            } },
            { "W", new List<int>()
            {
                -1,
                4
            } },
            { "A", new List<int>()
            {
                0,
                0
            } },
            { "C", new List<int>()
            {
                4,
                4
            } },
        };
        public static Dictionary<string, int> sides = new Dictionary<string, int>()
        {
            {"T", 0},
            {"N", 1},
            {"E", 2},
            {"S", 3},
            {"W", 4},
            {"B", 5},
            {"C", 6},
            {"A", 0}
        };
        public static List<string> sideLetters = new List<string>()
        {
            "T",
            "N",
            "E",
            "S",
            "W",
            "B",
            "C"
        };
        public static int selectedX = 3;
        public static int selectedY = 3;

        public static int selectedO = 0;

        public static int invselectedX = 0;
        public static int invselectedY = 0;
        public static ConsoleKeyInfo LastKey;
        public static List<KeyValuePair<string, string>> ConfigIDs = new List<KeyValuePair<string, string>>();
        public static string file = Environment.GetEnvironmentVariable("TEMP") + "\\GATE-LEVELS\\";
        public static List<int> enabledModules;

        public static bool enableCheat = false;
        public static string KeyPress;
        public static int KeyPress2;
        public static DateTime KeyPressTimer = DateTime.Now;
        public static bool secondLoop = false;
        public static DateTime secondLoopTimer = DateTime.Now;
        public static int secondSwitch = -1;
        public static int secondLoopCount = 0;

        public static Dictionary<int, List<int>> inputSignals = new Dictionary<int, List<int>>();

        public static List<string> generatorLevels = new List<string>()
        {
            "1;1,0.*=0.*~10000,PIPES;-84;-85,string1,",
            "1;2,0.*=0.*+1.*~10000,PIPES;-84;-85;LOGIC,string2,"
        };
        public static List<List<int>> levelInputs;
        public static List<List<int>> levelOutputs;
        public static List<string> winConditions = new List<string>();

        public static DateTime testTimestamp = DateTime.Now;
        public static int testTimeout = -1;
    }
    class Program
    {
        static void resetWorld(string preset = "MAINMENU4")
        {
            int oco = 0;
            Playground.enabledModules = new List<int>();
            foreach (string o in Playground.gates)
            {
                Playground.enabledModules.Add(oco);
                oco++;
            }
            Playground.signals = new List<List<List<List<int>>>>();
            Playground.signalCopy = new List<List<List<List<int>>>>();
            Playground.signalAge = new List<List<List<List<int>>>>();
            Playground.signalHold = new List<List<List<List<int>>>>();
            Playground.signalHoldCopy = new List<List<List<List<int>>>>();
            Playground.signalHoldAge = new List<List<List<List<int>>>>();
            Playground.signalHoldTimestamp = new List<List<List<List<DateTime>>>>();
            Playground.world = new List<List<List<int>>>();
            Playground.lockObj = new List<List<List<int>>>();
            for (int i1 = 0; i1 < Playground.layers; i1++)
            {
                Playground.world.Add(new List<List<int>>());
                Playground.lockObj.Add(new List<List<int>>());
                Playground.signals.Add(new List<List<List<int>>>());
                Playground.signalCopy.Add(new List<List<List<int>>>());
                Playground.signalAge.Add(new List<List<List<int>>>());
                Playground.signalHold.Add(new List<List<List<int>>>());
                Playground.signalHoldCopy.Add(new List<List<List<int>>>());
                Playground.signalHoldAge.Add(new List<List<List<int>>>());
                Playground.signalHoldTimestamp.Add(new List<List<List<DateTime>>>());
                for (int i2 = 0; i2 < Playground.width; i2++)
                {
                    Playground.world[i1].Add(new List<int>());
                    Playground.lockObj[i1].Add(new List<int>());
                    Playground.signals[i1].Add(new List<List<int>>());
                    Playground.signalCopy[i1].Add(new List<List<int>>());
                    Playground.signalAge[i1].Add(new List<List<int>>());
                    Playground.signalHold[i1].Add(new List<List<int>>());
                    Playground.signalHoldCopy[i1].Add(new List<List<int>>());
                    Playground.signalHoldAge[i1].Add(new List<List<int>>());
                    Playground.signalHoldTimestamp[i1].Add(new List<List<DateTime>>());
                    for (int i3 = 0; i3 < Playground.height; i3++)
                    {
                        Playground.signals[i1][i2].Add(new List<int>());
                        Playground.signalCopy[i1][i2].Add(new List<int>());
                        Playground.signalAge[i1][i2].Add(new List<int>());
                        Playground.signalHold[i1][i2].Add(new List<int>());
                        Playground.signalHoldCopy[i1][i2].Add(new List<int>());
                        Playground.signalHoldAge[i1][i2].Add(new List<int>());
                        Playground.signalHoldTimestamp[i1][i2].Add(new List<DateTime>());
                        Playground.world[i1][i2].Add(0);
                        Playground.lockObj[i1][i2].Add(0);
                        for (int i4 = 0; i4 < 7; i4++)
                        {
                            Playground.signals[i1][i2][i3].Add(0);
                            Playground.signalCopy[i1][i2][i3].Add(0);
                            Playground.signalAge[i1][i2][i3].Add(0);
                            Playground.signalHold[i1][i2][i3].Add(0);
                            Playground.signalHoldCopy[i1][i2][i3].Add(0);
                            Playground.signalHoldAge[i1][i2][i3].Add(0);
                            Playground.signalHoldTimestamp[i1][i2][i3].Add(DateTime.Now);
                        }
                    }
                }
            }
            switch (preset)
            {
                case "Playground":
                    Playground.world[Playground.baseLayer][1][1] = 112;
                    Playground.world[Playground.baseLayer][2][1] = 109;
                    Playground.world[Playground.baseLayer][3][1] = 109;
                    Playground.world[Playground.baseLayer][4][1] = 109;
                    Playground.world[Playground.baseLayer][5][1] = 109;
                    Playground.world[Playground.baseLayer][6][1] = 109;
                    break;
                case "START":
                    Playground.world[Playground.baseLayer][1][1] = 108;
                    Playground.world[Playground.baseLayer][2][1] = 109;
                    Playground.world[Playground.baseLayer][3][1] = 109;
                    Playground.world[Playground.baseLayer][4][1] = 109;
                    Playground.world[Playground.baseLayer][5][1] = 109;
                    Playground.world[Playground.baseLayer][6][1] = 109;
                    Playground.lockObj[Playground.baseLayer][1][1] = 1;
                    Playground.lockObj[Playground.baseLayer][2][1] = 1;
                    Playground.lockObj[Playground.baseLayer][3][1] = 1;
                    Playground.lockObj[Playground.baseLayer][4][1] = 1;
                    Playground.lockObj[Playground.baseLayer][5][1] = 1;
                    Playground.lockObj[Playground.baseLayer][6][1] = 1;

                    Playground.world[Playground.baseLayer][4][2] = 168;
                    Playground.world[Playground.baseLayer][4][3] = 51;
                    Playground.world[Playground.baseLayer][5][3] = 173;
                    Playground.world[Playground.baseLayer][4][4] = 169;
                    Playground.world[Playground.baseLayer][5][4] = 109;
                    Playground.world[Playground.baseLayer][4][5] = 51;
                    Playground.world[Playground.baseLayer][5][5] = 172;

                    Playground.world[Playground.baseLayer][1][3] = 24;
                    Playground.world[Playground.baseLayer][1][5] = 27;
                    Playground.world[Playground.baseLayer][3][3] = 25;
                    Playground.world[Playground.baseLayer][3][5] = 26;

                    Playground.world[Playground.baseLayer][2][3] = 37;
                    Playground.world[Playground.baseLayer][2][5] = 39;
                    Playground.world[Playground.baseLayer][1][4] = 36;
                    Playground.world[Playground.baseLayer][3][4] = 38;

                    Playground.signals[Playground.baseLayer][3][5][4] = 4;

                    Playground.enabledModules = new List<int>();
                    break;
                case "DEMO":
                    Playground.world[Playground.baseLayer][0][0] = 28;
                    Playground.world[Playground.baseLayer][0][1] = 27;

                    Playground.world[Playground.baseLayer][1][1] = 8;

                    Playground.world[Playground.baseLayer][2][0] = 28;
                    Playground.world[Playground.baseLayer][2][1] = 21;

                    Playground.world[Playground.baseLayer][1][2] = 34;
                    break;
                case "CLEAR":
                    break;
                case "TUT":
                    Playground.enabledModules = new List<int>();
                    Playground.world[Playground.baseLayer][0][0] = 352;
                    Playground.world[Playground.baseLayer][0][1] = 353;
                    Playground.world[Playground.baseLayer][0][2] = 354;
                    Playground.world[Playground.baseLayer][0][3] = 355;
                    Playground.world[Playground.baseLayer][0][4] = 356;

                    Playground.world[Playground.baseLayer][5][1] = 357;
                    Playground.world[Playground.baseLayer][5][2] = 109;
                    Playground.world[Playground.baseLayer][6][1] = 109;
                    Playground.world[Playground.baseLayer][6][2] = 109;
                    Playground.world[Playground.baseLayer][7][1] = 109;
                    Playground.world[Playground.baseLayer][7][2] = 109;
                    Playground.world[Playground.baseLayer][8][1] = 109;
                    Playground.world[Playground.baseLayer][8][2] = 109;
                    Playground.world[Playground.baseLayer][9][1] = 109;
                    Playground.world[Playground.baseLayer][9][2] = 109;
                    Playground.world[Playground.baseLayer][10][1] = 109;
                    Playground.world[Playground.baseLayer][10][2] = 109;

                    Playground.world[Playground.baseLayer][1][0] = 110;
                    Playground.world[Playground.baseLayer][1][1] = 110;
                    Playground.world[Playground.baseLayer][1][2] = 110;
                    Playground.world[Playground.baseLayer][1][3] = 110;
                    Playground.world[Playground.baseLayer][1][4] = 110;

                    Playground.world[Playground.baseLayer][2][0] = 110;
                    Playground.world[Playground.baseLayer][2][1] = 110;
                    Playground.world[Playground.baseLayer][2][2] = 110;
                    Playground.world[Playground.baseLayer][2][3] = 110;
                    Playground.world[Playground.baseLayer][2][4] = 110;

                    Playground.world[Playground.baseLayer][3][0] = 110;
                    Playground.world[Playground.baseLayer][3][1] = 110;
                    Playground.world[Playground.baseLayer][3][2] = 110;
                    Playground.world[Playground.baseLayer][3][3] = 110;
                    Playground.world[Playground.baseLayer][3][4] = 110;

                    for (int i3 = 0; i3 < 7; i3++)
                    {
                        for (int i1 = 5; i1 <= 9; i1++)
                        {
                            for (int i2 = 4; i2 <= 6; i2++)
                            {
                                Playground.world[i3][i1][i2] = 110;
                            }
                        }
                    }

                    Playground.world[0][5][4] = 360;
                    Playground.world[0][5][5] = 370;

                    Playground.world[1][5][4] = 361;
                    Playground.world[1][5][5] = 369;

                    Playground.world[2][5][4] = 362;
                    Playground.world[2][5][5] = 368;

                    Playground.world[3][5][4] = 363;
                    Playground.world[3][5][5] = 367;
                    
                    Playground.world[4][5][5] = 366;
                    
                    Playground.world[5][5][5] = 365;
                    
                    Playground.world[6][5][5] = 364;
                    break;
                default:
                    loadFromFile(preset);
                    break;
            }
        }
        static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                Playground.file = args[0] + "\\";
            }
            resetWorld();
            while (true)
            {
                if (DateTime.Now.AddMilliseconds(-1000).CompareTo(Playground.secondLoopTimer) > 0)
                {
                    Playground.secondLoop = true;
                    Playground.secondLoopTimer = DateTime.Now;
                    Playground.secondSwitch *= -1;
                    Playground.secondLoopCount++;
                }
                else
                {
                    Playground.secondLoop = false;
                }
                if (DateTime.Now.AddMilliseconds(-500).CompareTo(Playground.KeyPressTimer) > 0)
                {
                    Playground.KeyPress2 = 0;
                }
                if (DateTime.Now.AddMilliseconds(-1*Playground.testTimeout).CompareTo(Playground.testTimestamp) > 0 && Playground.testTimeout != -1)
                {
                    Playground.xMode = false;
                }
                Console.CursorVisible = false;
                Playground.displayStrings = new List<string>();
                for (int j1 = 0; j1 < Playground.layers; j1++)
                {
                    for (int j2 = 0; j2 < Playground.width; j2++)
                    {
                        for (int j3 = 0; j3 < Playground.height; j3++)
                        {
                            lowerSignals(j1, j2, j3);
                            lowerSignals(Playground.layers - 1 - j1, Playground.width - 1 - j2, Playground.height - 1 - j3);
                        }
                    }
                }
                for (int j1 = 0; j1 < Playground.layers; j1++)
                {
                    for (int j2 = 0; j2 < Playground.width; j2++)
                    {
                        for (int j3 = 0; j3 < Playground.height; j3++)
                        {
                            checkSignals(j1, j2, j3);
                            checkSignals(Playground.layers - 1 - j1, Playground.width - 1 - j2, Playground.height - 1 - j3);
                        }
                    }
                }
                for (int j1 = 0; j1 < Playground.layers; j1++)
                {
                    for (int j2 = 0; j2 < Playground.width; j2++)
                    {
                        for (int j3 = 0; j3 < Playground.height; j3++)
                        {
                            lowerSignals(j1, j2, j3, false, true);
                            lowerSignals(Playground.layers - 1 - j1, Playground.width - 1 - j2, Playground.height - 1 - j3, false, true);
                        }
                    }
                }
                Playground.signalHoldAge = Playground.signals;
                for (int j1 = 0; j1 < Playground.layers; j1++)
                {
                    for (int j2 = 0; j2 < Playground.width; j2++)
                    {
                        for (int j3 = 0; j3 < Playground.height; j3++)
                        {
                            copySignals(j1, j2, j3);
                            copySignals(Playground.layers - 1 - j1, Playground.width - 1 - j2, Playground.height - 1 - j3);
                        }
                    }
                }
                Playground.signals = Playground.signalHoldAge;
                //Playground.signalHoldAge = Playground.signals;
                //for (int j1 = Playground.layers - 1; j1 > -1; j1--)
                //{
                //    for (int j2 = Playground.width - 1; j2 > -1; j2--)
                //    {
                //        for (int j3 = Playground.height - 1; j3 > -1; j3--)
                //        {
                //            copySignals(j1, j2, j3);
                //        }
                //    }
                //}
                //Playground.signals = Playground.signalHoldAge;
                for (int j1 = 0; j1 < Playground.layers; j1++)
                {
                    for (int j2 = 0; j2 < Playground.width; j2++)
                    {
                        for (int j3 = 0; j3 < Playground.height; j3++)
                        {
                            if (j1 == Playground.LoadedLayer)
                            {
                                display(j1, j2, j3);
                                displayStrings(j1);
                            }
                            Console.SetCursorPosition(0, 0);
                            Console.Write("Layer {0,-3}", (Playground.LoadedLayer - Playground.baseLayer));
                        }
                    }
                }
                Playground.KeyPress2 = 0;
                Thread.Sleep((int)(1000 / Playground.TicksPerSecond));
                if (!Playground.xMode)
                {
                    Playground.activeTest = -1;
                }
                if (Playground.xMode && Playground.activeTest < 0)
                {
                    Playground.activeTest = 0;
                }
                if (Playground.activeTest >= 0)
                {
                    bool won = false;
                    if (Playground.activeTest < Playground.winConditions.Count)
                    {
                        won = checkTest(Playground.activeTest);
                    }
                    if (won)
                    {
                        Playground.testTimeout = -1;
                        Playground.testTimestamp = DateTime.Now;
                        if (!((Playground.activeTest + 1) >= Playground.winConditions.Count))
                        {
                            initTest(Playground.activeTest + 1);
                            Playground.activeTest++;
                            int test = 2;
                        }
                        else
                        {
                            Console.SetCursorPosition(10, 16);
                            Console.Write("SSSSSSSSSS  UU      UU  CCCCCCCCCC  CCCCCCCCCC  EEEEEEEEEE  SSSSSSSSSS  SSSSSSSSSS");
                            Console.SetCursorPosition(10, 17);
                            Console.Write("SSSSSSSSSS  UU      UU  CCCCCCCCCC  CCCCCCCCCC  EEEEEEEEEE  SSSSSSSSSS  SSSSSSSSSS");
                            Console.SetCursorPosition(10, 18);
                            Console.Write("SS          UU      UU  CC          CC          EE          SS          SS        ");
                            Console.SetCursorPosition(10, 19);
                            Console.Write("SS          UU      UU  CC          CC          EE          SS          SS        ");
                            Console.SetCursorPosition(10, 20);
                            Console.Write("SSSSSSSSSS  UU      UU  CC          CC          EEEEEE      SSSSSSSSSS  SSSSSSSSSS");
                            Console.SetCursorPosition(10, 21);
                            Console.Write("SSSSSSSSSS  UU      UU  CC          CC          EEEEEE      SSSSSSSSSS  SSSSSSSSSS");
                            Console.SetCursorPosition(10, 22);
                            Console.Write("        SS  UU      UU  CC          CC          EE                  SS          SS");
                            Console.SetCursorPosition(10, 23);
                            Console.Write("        SS  UU      UU  CC          CC          EE                  SS          SS");
                            Console.SetCursorPosition(10, 24);
                            Console.Write("SSSSSSSSSS  UUUUUUUUUU  CCCCCCCCCC  CCCCCCCCCC  EEEEEEEEEE  SSSSSSSSSS  SSSSSSSSSS");
                            Console.SetCursorPosition(10, 25);
                            Console.Write("SSSSSSSSSS  UUUUUUUUUU  CCCCCCCCCC  CCCCCCCCCC  EEEEEEEEEE  SSSSSSSSSS  SSSSSSSSSS");
                            Playground.xMode = false;
                            while (Console.KeyAvailable)
                            {
                                Console.ReadKey();
                            }
                            Console.ReadKey();
                            saveToFile("SOLUTION-" + Playground.levelID);
                            if (Playground.nextLvl != "")
                            {
                                resetWorld(Playground.nextLvl);
                            }
                        }
                    }
                }
                bool acceptkey = !Playground.xMode;
                bool dE = false;
                if (Playground.world[Playground.LoadedLayer][Playground.selectedX][Playground.selectedY] < 4)
                {
                    dE = true;
                }
                info_screen(dE);
                while (Console.KeyAvailable)
                {
                    ConsoleKeyInfo cki = Console.ReadKey(true);
                    if (Playground.xMode && cki.Key == ConsoleKey.X)
                    {
                        Playground.xMode = false;
                    }
                    if (acceptkey)
                    {
                        acceptkey = false;
                        switch (Playground.LastKey.Key)
                        {
                            case ConsoleKey.D:
                                switch (cki.Key)
                                {
                                    case ConsoleKey.D1:
                                        configManagerChange(0);
                                        break;
                                    case ConsoleKey.D2:
                                        configManagerChange(1);
                                        break;
                                    case ConsoleKey.D3:
                                        configManagerChange(2);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            default:
                                switch (cki.Key)
                                {
                                    case ConsoleKey.I:
                                        if (Convert.ToInt32(cki.Modifiers) == 7)
                                        {
                                            if (!Playground.enableCheat)
                                            {
                                                Playground.enableCheat = true;
                                                Playground.Spacing = 1;
                                            }
                                            else
                                            {
                                                Playground.enableCheat = false;
                                                Playground.Spacing = 0;
                                            }
                                            Console.Clear();
                                        }
                                        break;
                                    case ConsoleKey.LeftArrow:
                                        Playground.selectedX--;

                                        break;
                                    case ConsoleKey.RightArrow:
                                        Playground.selectedX++;
                                        break;
                                    case ConsoleKey.UpArrow:
                                        Playground.selectedY--;
                                        break;
                                    case ConsoleKey.DownArrow:
                                        Playground.selectedY++;
                                        break;
                                    case ConsoleKey.Enter:
                                        place();
                                        break;
                                    case ConsoleKey.Q:
                                        if (Playground.enableCheat)
                                        {
                                            Playground.selectedO -= 4;
                                        }
                                        break;
                                    case ConsoleKey.E:
                                        if (Playground.enableCheat)
                                        {
                                            Playground.selectedO += 4;
                                        }
                                        break;
                                    case ConsoleKey.Y:
                                        if (Playground.selectedO % 4 != 0)
                                        {
                                            Playground.selectedO--;
                                        }
                                        else
                                        {
                                            Playground.selectedO += 3;
                                        }
                                        break;
                                    case ConsoleKey.C:
                                        if (Playground.selectedO % 4 != 3)
                                        {
                                            Playground.selectedO++;
                                        }
                                        else
                                        {
                                            Playground.selectedO -= 3;
                                        }
                                        break;
                                    case ConsoleKey.Spacebar:
                                        Playground.signals[Playground.LoadedLayer][Playground.selectedX][Playground.selectedY][6] = 3;
                                        break;
                                    case ConsoleKey.W:
                                        Playground.LoadedLayer++;
                                        Console.Clear();
                                        break;
                                    case ConsoleKey.S:
                                        Playground.LoadedLayer--;
                                        Console.Clear();
                                        break;
                                    case ConsoleKey.D:
                                        configManager();
                                        break;
                                    case ConsoleKey.A:
                                        Inventory();
                                        break;
                                    case ConsoleKey.X:
                                        Playground.xMode = true;
                                        if (Playground.winConditions.Count > 0)
                                        {
                                            initTest(0);
                                        }
                                        break;
                                    default:
                                        break;
                                }
                                break;
                        }
                    }
                    int zeros = 8 - (Convert.ToString(Convert.ToInt32(cki.Key), 2)).Length;
                    Playground.KeyPress = "00000000".Substring(0, zeros) + (Convert.ToString(Convert.ToInt32(cki.Key), 2));
                    Playground.KeyPress2 = Convert.ToInt32(cki.Key);
                    Playground.KeyPressTimer = DateTime.Now;
                    if (Playground.selectedO >= Playground.gates.Count)
                    {
                        Playground.selectedO = 0;
                    }
                    if (Playground.selectedO < 0)
                    {
                        Playground.selectedO = Playground.gates.Count - 4;
                    }
                    if (Playground.selectedX >= Playground.width)
                    {
                        Playground.selectedX = 0;
                    }
                    if (Playground.selectedX < 0)
                    {
                        Playground.selectedX = Playground.width - 1;
                    }
                    if (Playground.selectedY >= Playground.height)
                    {
                        Playground.selectedY = 0;
                    }
                    if (Playground.selectedY < 0)
                    {
                        Playground.selectedY = Playground.height - 1;
                    }
                    if (Playground.LoadedLayer < 0)
                    {
                        Playground.LoadedLayer = 0;
                    }
                    if (Playground.LoadedLayer >= Playground.layers)
                    {
                        Playground.LoadedLayer = Playground.layers - 1;
                    }
                    Playground.LastKey = cki;
                }
            }
        }
        static void place()
        {
            if (Playground.lockObj[Playground.LoadedLayer][Playground.selectedX][Playground.selectedY] == 0)
            {
                if (Playground.world[Playground.LoadedLayer][Playground.selectedX][Playground.selectedY] > 3)
                {
                    Playground.world[Playground.LoadedLayer][Playground.selectedX][Playground.selectedY] = 0;
                    lowerSignals(Playground.LoadedLayer, Playground.selectedX, Playground.selectedY, true);
                }
                else
                {
                    Playground.world[Playground.LoadedLayer][Playground.selectedX][Playground.selectedY] = Playground.selectedO;
                }
            }
        }
        static void lowerSignals(int layer, int x, int y, bool full = false, bool second = false)
        {
            for (int i = 0; i < 7; i++)
            {
                if (full)
                {
                    Playground.signals[layer][x][y][i] = 0;
                    Playground.signalCopy[layer][x][y][i] = 0;
                    Playground.signalAge[layer][x][y][i] = 0;
                    Playground.signalHold[layer][x][y][i] = 0;
                    Playground.signalHoldCopy[layer][x][y][i] = 0;
                    Playground.signalHoldAge[layer][x][y][i] = 0;
                    Playground.signalHoldTimestamp[layer][x][y][i] = DateTime.Now;
                }
                else
                {
                    if (Playground.signals[layer][x][y][i] > 0)
                    {
                        if (!second)
                        {
                            if (Playground.timingGates.ContainsKey(Playground.world[layer][x][y]))
                            {
                                Playground.signalHold[layer][x][y][i] = Playground.signals[layer][x][y][i];
                            }
                            Playground.signals[layer][x][y][i]--;
                        }
                        if (Playground.inputSignals.ContainsKey(Playground.world[layer][x][y]))
                        {
                            if (Playground.inputSignals[Playground.world[layer][x][y]].Contains(i))
                            {
                                if (Playground.signalAge[layer][x][y][i] >= 2)
                                { // Playground.neighbourSignals[i]
                                    if (Playground.signals[layer + Playground.neighbours[i][0]][x + Playground.neighbours[i][1]][y + Playground.neighbours[i][2]][Playground.neighbourSignals[i]] <= 2)
                                    {
                                      Playground.signals[layer][x][y][i] = 0;
                                    }
                                    else
                                    {
                                    }
                                }
                                else
                                {
                                    int o = 0;
                                    o++;
                                }

                            }
                            else
                            {
                                if (!second)
                                {
                                 // Playground.signals[layer][x][y][i] = 0;
                                }
                            }
                        } else
                        {
                            if (!second)
                            {
                                Playground.signals[layer][x][y][i] = 0;
                            }
                        }
                    }
                    if (!second || true)
                    {
                        if (Playground.signalCopy[layer][x][y][i] == Playground.signals[layer][x][y][i] && Playground.signals[layer][x][y][i] != 0)
                        {
                            Playground.signalAge[layer][x][y][i]++;
                        }
                        else
                        {
                            Playground.signalAge[layer][x][y][i] = 1;
                        }
                        if (Playground.signalHoldCopy[layer][x][y][i] == Playground.signalHold[layer][x][y][i] && Playground.signalHold[layer][x][y][i] != 0)
                        {
                            // Playground.signalHoldAge[layer][x][y][i]++;
                        }
                        else
                        {
                            // Playground.signalHoldAge[layer][x][y][i] = 1;
                            Playground.signalHoldTimestamp[layer][x][y][i] = DateTime.Now;
                        }
                        Playground.signalCopy[layer][x][y][i] = Playground.signals[layer][x][y][i];
                        Playground.signalHoldCopy[layer][x][y][i] = Playground.signalHold[layer][x][y][i];
                    }
                }
            }
        }
        static void copySignals(int layer, int x, int y)
        {
            for (int i = 0; i < 7; i++)
            {
                bool copy = true;
                if (Playground.inputSignals.ContainsKey(Playground.world[layer][x][y]))
                {
                    if (Playground.inputSignals[Playground.world[layer][x][y]].Contains(i))
                    {
                        copy = false;
                    }
                }
                if (Playground.signals[layer][x][y][i] >= 3 && copy)
                {
                    try
                    {
                        Playground.signalHoldAge[layer + Playground.neighbours[i][0]][x + Playground.neighbours[i][1]][y + Playground.neighbours[i][2]][Playground.neighbourSignals[i]] = (Playground.signals[layer][x][y][i] > 0) ? Playground.signals[layer][x][y][i] : 0;
                    }
                    catch (System.ArgumentOutOfRangeException) { }
                }
            }
        }
        static void checkSignals(int layer, int x, int y)
        {
            int posX = (5 + Playground.Spacing) * x + 1;
            int posY = (5 + Playground.Spacing) * y + 1;
            string display = Playground.gates[Playground.world[layer][x][y]];
            string[] options = display.Split(',');
            string[] drows = options[0].Split(';');
            string[] io = options[1].Split('=');
            bool makeOutput = false;
            bool reverseOutput = false;
            int outputStrength = 2;
            if (io[0] == "")
            {
                makeOutput = true;
            }
            else
            {
                if (io[0].Substring(0, 1) == "!")
                {
                    io[0] = io[0].Substring(1);
                    reverseOutput = true;
                }
                if (io[0].Split(' ').Length == 1)
                {
                    if (Playground.signals[layer][x][y][Playground.sides[io[0]]] > 0)
                    {
                        makeOutput = true;
                    }
                }
                else
                {
                    string method = io[0].Split(' ')[1];
                    int signal1 = Playground.signals[layer][x][y][Playground.sides[io[0].Split(' ')[0]]];
                    int signal2;
                    if (method != ".")
                    {
                        signal2 = Playground.signals[layer][x][y][Playground.sides[io[0].Split(' ')[2]]];
                    }
                    else
                    {
                        signal2 = Playground.timingGates[Playground.world[layer][x][y]] * 1000;
                    }
                    switch (method)
                    {
                        case "+":
                            makeOutput = (signal1 > 0 && signal2 > 0);
                            break;
                        case "/":
                            makeOutput = (signal1 > 0 || signal2 > 0);
                            break;
                        case ".":
                            if (signal1 > 0)
                            {
                                Playground.signalHold[layer][x][y][Playground.sides[io[0].Split(' ')[0]]] = signal1;
                            }
                            if (Playground.signalHoldTimestamp[layer][x][y][Playground.sides[io[0].Split(' ')[0]]].AddMilliseconds(signal2).CompareTo(DateTime.Now) < 0 && Playground.signalHold[layer][x][y][Playground.sides[io[0].Split(' ')[0]]] > 0)
                            {
                                makeOutput = true;
                                Playground.signalHold[layer][x][y][0] = 0;
                                Playground.signalHold[layer][x][y][1] = 0;
                                Playground.signalHold[layer][x][y][2] = 0;
                                Playground.signalHold[layer][x][y][3] = 0;
                                Playground.signalHold[layer][x][y][4] = 0;
                                Playground.signalHold[layer][x][y][5] = 0;
                                Playground.signalHold[layer][x][y][6] = 0;
                            }
                            break;
                        case "D&":
                            makeOutput = (signal1 > 0 && signal2 > 0);
                            outputStrength = Math.Min(signal1, signal2);
                            break;
                        case "D|":
                            makeOutput = (signal1 > 0 || signal2 > 0);
                            outputStrength = Math.Max(signal1, signal2);
                            break;
                        case "D+":
                            makeOutput = (signal1 > 0 || signal2 > 0);
                            int sub = 1;
                            if (signal1 == 0 || signal2 == 0)
                            {
                                sub = 0;
                            }
                            outputStrength = signal1 + signal2 - sub;
                            break;
                        case "Dv":
                            makeOutput = true;
                            outputStrength = 1;
                            break;
                        case "Dd":
                            makeOutput = true;
                            outputStrength = 6;
                            break;
                        case "DC":
                            makeOutput = (signal1 == signal2 && signal1 > 0);
                            outputStrength = signal1;
                            break;
                        case "Dk":
                            makeOutput = (signal1 > 0 && signal2 > 0);
                            outputStrength = signal2;
                            break;
                        case "levelin":
                            makeOutput = (signal1 > 0 || signal2 > 0);
                            outputStrength = Math.Max(signal1, signal2);
                            break;
                        default:
                            if (method.Substring(0, 1) == "F")
                            {
                                switch (method.Substring(1, 4))
                                {
                                    case "key1":
                                        makeOutput = ((char)Playground.KeyPress[7] == '1');
                                        break;
                                    case "key2":
                                        makeOutput = ((char)Playground.KeyPress[6] == '1');
                                        break;
                                    case "key3":
                                        makeOutput = ((char)Playground.KeyPress[5] == '1');
                                        break;
                                    case "key4":
                                        makeOutput = ((char)Playground.KeyPress[4] == '1');
                                        break;
                                    case "key5":
                                        makeOutput = ((char)Playground.KeyPress[3] == '1');
                                        break;
                                    case "key6":
                                        makeOutput = ((char)Playground.KeyPress[2] == '1');
                                        break;
                                    case "key7":
                                        makeOutput = ((char)Playground.KeyPress[1] == '1');
                                        break;
                                    case "key8":
                                        makeOutput = ((char)Playground.KeyPress[0] == '1');
                                        break;
                                    case "keyr":
                                        makeOutput = (Playground.KeyPress2 > 0);
                                        outputStrength = Playground.KeyPress2 + 1;
                                        break;
                                    case "dcle":
                                        Playground.displayStrings.Add(layer + " " + (posX + 2) + " " + (posY + 3) + " 1 \u2591");
                                        break;
                                }
                            }
                            break;
                    }
                }
            }
            if (makeOutput != reverseOutput && io.Length == 2)
            {
                foreach (string side in io[1].Split('+'))
                {
                    if (side.Substring(0, 1) == "A")
                    {
                        switch (side.Substring(1, 4))
                        {
                            case "Beep":
                                Console.Beep();
                                break;
                            case "LoWo":
                                resetWorld(side.Substring(5));
                                break;
                            case "ShDa":
                                Playground.displayStrings.Add(layer + " " + (posX + 1) + " " + (posY + 2) + " 3 " + (outputStrength - 1));
                                break;
                            case "DaEm":
                                outputStrength = Int32.Parse(side.Substring(5)) + 1;
                                break;
                            case "Disp":
                                Playground.displayStrings.Add(layer + " " + (posX) + " " + (posY + 0) + " 5 " + "\u2593");
                                Playground.displayStrings.Add(layer + " " + (posX) + " " + (posY + 1) + " 5 " + "\u2593");
                                Playground.displayStrings.Add(layer + " " + (posX) + " " + (posY + 2) + " 5 " + "\u2593");
                                Playground.displayStrings.Add(layer + " " + (posX) + " " + (posY + 3) + " 5 " + "\u2593");
                                Playground.displayStrings.Add(layer + " " + (posX) + " " + (posY + 4) + " 5 " + "\u2593");
                                break;
                        }
                    }
                    else
                    {
                        Playground.signals[layer][x][y][Playground.sides[side.Substring(0, 1)]] = outputStrength + 2;
                    }
                }
            } else if(io.Length == 2)
            {
                foreach (string side in io[1].Split('+'))
                {
                    Playground.signals[layer][x][y][Playground.sides[side.Substring(0, 1)]] = 0;
                }
            }
        }
        static void display(int layer, int x, int y, bool selMode = false)
        {
            int posX = (5 + Playground.Spacing) * x + 1;
            int posY = (5 + Playground.Spacing) * y + 1;
            int objid;
            int objid2;
            bool isselected;
            Console.ForegroundColor = ConsoleColor.White;
            if (!selMode)
            {
                objid = Playground.world[layer][x][y];
                objid2 = objid;
                isselected = false;
                if (x == Playground.selectedX && y == Playground.selectedY)
                {
                    isselected = true;
                }
                if (objid < 4)
                {
                    if (isselected)
                    {
                        objid = Playground.selectedO;
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                    }
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
                posX = x;
                posY = y;
                objid = layer;
                objid2 = layer;
                isselected = false;
            }
            if(objid >= 336 && objid <= 343 && Playground.xMode)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            string display = Playground.gates[objid];
            if (isselected && (objid2 > 3 || Playground.selectedO < 4))
            {
                display = "X" + display.Substring(1);
            }
            string[] options = display.Split(',');
            string[] drows = options[0].Split(';');
            string[] io = options[1].Split('=');
            int crow = -1;
            foreach (string row in drows)
            {
                crow++;
                Console.SetCursorPosition(posX, posY + crow);
                Console.Write(row.Replace("#", "\u2591").Replace("COMMA", ",").Replace("~", "\u2593"));
            }
            if (io[0].Split(' ').Length == 1)
            {
                if (io[0] != "")
                {
                    if (io[0].Substring(0, 1) == "!")
                    {
                        io[0] = io[0].Substring(1);
                    }
                    if (io[0] != "")
                    {
                        int ss;
                        if (selMode)
                        {
                            ss = 0;
                        }
                        else
                        {
                            ss = Playground.signals[layer][x][y][Playground.sides[io[0]]];
                        }
                        showsignal(0, io[0], ss, layer, x, y, selMode);
                    }
                }
            }
            else if (io[0].Split(' ').Length == 3)
            {
                string side1 = io[0].Split(' ')[0];
                string side2 = io[0].Split(' ')[2];
                int ss1;
                int ss2;
                if (selMode)
                {
                    ss1 = 0;
                    ss2 = 0;
                }
                else
                {
                    ss2 = Playground.signals[layer][x][y][Playground.sides[side2.Substring(0, 1)]];
                    ss1 = Playground.signals[layer][x][y][Playground.sides[side1.Substring(0, 1)]];
                }
                showsignal(0, side1, ss1, layer, x, y, selMode);
                showsignal(0, side2, ss2, layer, x, y, selMode);
            }
            if (io.Length == 2)
            {
                if (io[1] != "")
                {
                    foreach (string side in io[1].Split('+'))
                    {
                        int ss; ;
                        if (selMode)
                        {
                            ss = 0;
                        }
                        else
                        {
                            ss = Playground.signals[layer][x][y][Playground.sides[side.Substring(0, 1)]];
                        }
                        showsignal(1, side, ss, layer, x, y, selMode);
                    }
                }
            }
            if (isselected)
            {
                Console.ForegroundColor = ConsoleColor.White;
            }
            if (Playground.xMode)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        static void showsignal(int mode, string side, int signalstrength, int layer, int x, int y, bool selMode = false)
        {
            if (mode == 0 && !selMode)
            {
                if (!Playground.inputSignals.ContainsKey(Playground.world[layer][x][y]))
                {
                    Playground.inputSignals.Add(Playground.world[layer][x][y], new List<int>());
                }
                Playground.inputSignals[Playground.world[layer][x][y]].Add(Playground.sides[side.Substring(0, 1)]);
            }
            if (side.Substring(0, 1) != "A")
            {
                ConsoleColor isactive = ConsoleColor.DarkGreen;
                ConsoleColor inactive = ConsoleColor.DarkRed;
                if (mode == 0)
                {
                    isactive = ConsoleColor.DarkGreen;
                    inactive = ConsoleColor.DarkBlue;
                }
                int Objid = 0;
                if (!selMode)
                {
                    Objid = Playground.world[layer][x][y];
                }
                else
                {
                    Objid = layer;
                }
                if (Playground.dataModules.Contains(Objid) && !Playground.noDataSides.Contains(Objid + "-" + side))
                {
                    isactive = ConsoleColor.Green;
                    inactive = ConsoleColor.Magenta;
                    if (mode == 0)
                    {
                        isactive = ConsoleColor.Green;
                        inactive = ConsoleColor.DarkYellow;
                    }
                }
                ConsoleColor color = isactive;
                int ss;
                if (!selMode)
                {
                    ss = Playground.signalHold[layer][x][y][Playground.sides[side.Substring(0, 1)]];
                }
                else
                {
                    ss = 0;
                }
                if (signalstrength == 0 && (ss == 0 || mode == 1))
                {
                    color = inactive;
                }
                int posX;
                int posY;
                if (!selMode)
                {
                    posX = (5 + Playground.Spacing) * x + 1;
                    posY = (5 + Playground.Spacing) * y + 1;
                }
                else
                {
                    posX = x;
                    posY = y;
                }
                if (Playground.enableCheat)
                {
                    Console.SetCursorPosition(posX + Playground.debugDisplays[side][0], posY + Playground.debugDisplays[side][1]);
                    if (Playground.secondSwitch > 0)
                    {
                        Console.Write(side);
                    }
                    else
                    {
                        Console.Write(signalstrength);
                    }
                }
                List<int> pos = Playground.sideDisplays[side.Substring(0, 1)];
                Console.SetCursorPosition(posX + pos[0], posY + pos[1]);
                ConsoleColor sc = Console.ForegroundColor;
                Console.ForegroundColor = color;
                Console.Write("\u2591");
                Console.ForegroundColor = sc;
            }
        }
        static void configManager()
        {
            Dictionary<string, string> settings = new Dictionary<string, string>() {
                {"Ticks Per Second", Playground.TicksPerSecond.ToString()},
                {"Load World", Playground.resetWorld.ToString() },
                {"Save World To", "" }
            };
            int rowc = 0;
            foreach (KeyValuePair<string, string> value in settings)
            {
                Playground.ConfigIDs.Add(value);
                rowc++;
                Console.SetCursorPosition((6 * Playground.width) + 1, rowc + 1);
                Console.Write("{2,-3} {0,-16} = {1}", value.Key, value.Value, rowc + ".");
            }
        }
        static void configManagerChange(int id)
        {
            Console.SetCursorPosition((6 * Playground.width) + 24, id + 2);
            Console.Write("                ");
            Console.SetCursorPosition((6 * Playground.width) + 24, id + 2);
            string input = Console.ReadLine();
            switch (Playground.ConfigIDs[id].Key)
            {
                case "Ticks Per Second":
                    Playground.TicksPerSecond = Double.Parse(input);
                    break;
                case "Load World":
                    resetWorld(input);
                    break;
                case "Save World To":
                    saveToFile(input);
                    break;
            }
            Console.Clear();
        }
        static void Inventory()
        {
            Console.Clear();
            Dictionary<string, int> selectedVariants = new Dictionary<string, int>();
            bool loopInv = true;
            while (loopInv)
            {
                int count = 0;
                int selected = 0;
                int biggestVariant = 1;
                List<string> collections = new List<string>();
                foreach (KeyValuePair<string, Dictionary<string, int>> group in Playground.selector)
                {
                    collections.Add(group.Key);
                    int selectedVariant;
                    if (selectedVariants.ContainsKey(group.Key))
                    {
                        selectedVariant = selectedVariants[group.Key];
                    }
                    else
                    {
                        selectedVariants[group.Key] = 1;
                        selectedVariant = selectedVariants[group.Key];
                    }
                    List<string> foundNames = new List<string>();
                    Dictionary<string, int> variantCounts = new Dictionary<string, int>();
                    Console.SetCursorPosition(2, (count * 9) + 2);
                    Console.Write(group.Key);
                    int count2 = 0;
                    foreach (KeyValuePair<string, int> obj in group.Value)
                    {
                        string actualname = obj.Key;
                        if (obj.Key.Length > 3)
                        {
                            if (obj.Key.Substring(obj.Key.Length - 3, 1) == "-")
                            {
                                actualname = obj.Key.Substring(0, obj.Key.Length - 3);
                                if (variantCounts.ContainsKey(actualname))
                                {
                                    variantCounts[actualname]++;
                                }
                            }
                        }
                        if (!variantCounts.ContainsKey(actualname))
                        {
                            variantCounts[actualname] = 1;
                        }
                    }
                    foreach (KeyValuePair<string, int> obj in group.Value)
                    {
                        int variant = 1;
                        string actualname = obj.Key;
                        int subtract = 0;
                        bool isbasic = true;
                        if (obj.Key.Length > 3)
                        {
                            if (obj.Key.Substring(obj.Key.Length - 3, 1) == "-")
                            {
                                variant = Int32.Parse(obj.Key.Substring(obj.Key.Length - 2));
                                if (variant > biggestVariant && count == Playground.invselectedY)
                                {
                                    biggestVariant = variant;
                                }
                                actualname = obj.Key.Substring(0, obj.Key.Length - 3);
                                isbasic = false;
                                if (variant > 1)
                                {
                                    subtract = 1;
                                }
                            }
                        }
                        if (variant == selectedVariant || isbasic || (selectedVariant > variantCounts[actualname] && variantCounts[actualname] == variant))
                        {
                            if (!Playground.enabledModules.Contains(obj.Value))
                            {
                                Console.BackgroundColor = ConsoleColor.Red;
                            }
                            display(obj.Value, 3 + ((count2 - subtract) * 10), (count * 9) + 4, true);
                            if (!Playground.enabledModules.Contains(obj.Value))
                            {
                                Console.BackgroundColor = ConsoleColor.Black;
                            }
                            if ((Playground.invselectedX == count2 && Playground.invselectedY == count && variant == 1) || ((Playground.invselectedX + 1) == count2 && Playground.invselectedY == count && variant > 1))
                            {
                                Console.SetCursorPosition(3 + ((count2 - subtract) * 10), (count * 9) + 4);
                                Console.Write("X");
                                selected = obj.Value;
                            }
                            if (false)
                            {
                                Console.SetCursorPosition(5 + ((count2 - subtract) * 10), (count * 9) + 10);
                                Console.Write(variant);
                            }
                            Console.SetCursorPosition(2 + ((count2 - subtract) * 10), (count * 9) + 9);
                            Console.Write(actualname);
                        }
                        if (!foundNames.Contains(actualname))
                        {
                            count2++;
                            foundNames.Add(actualname);
                        }
                    }
                    count++;
                }
                if (selected == 0)
                {
                    Playground.invselectedX = 0;
                    Playground.invselectedY++;
                }
                if (Playground.invselectedY >= Playground.selector.Count)
                {
                    Playground.invselectedY = 0;
                }
                ConsoleKey cki = ConsoleKey.E;
                bool x = false;
                info_screen(true, selected);
                if (Console.KeyAvailable)
                {
                    cki = Console.ReadKey(true).Key;
                    x = true;
                }
                if (x)
                {
                    switch (cki)
                    {
                        case ConsoleKey.LeftArrow:
                            if (Playground.invselectedX != 0)
                            {
                                Playground.invselectedX--;
                            }
                            break;
                        case ConsoleKey.RightArrow:
                            Playground.invselectedX++;
                            break;
                        case ConsoleKey.UpArrow:
                            if (Playground.invselectedY != 0)
                            {
                                selectedVariants[collections[Playground.invselectedY]] = 1;
                                Playground.invselectedY--;
                            }
                            break;
                        case ConsoleKey.DownArrow:
                            selectedVariants[collections[Playground.invselectedY]] = 1;
                            Playground.invselectedY++;
                            break;
                        case ConsoleKey.Enter:
                            if (Playground.enabledModules.Contains(selected))
                            {
                                Playground.selectedO = selected;
                                loopInv = false;
                            }
                            break;
                        case ConsoleKey.A:
                            loopInv = false;
                            break;
                        case ConsoleKey.Escape:
                            loopInv = false;
                            break;
                        case ConsoleKey.Q:
                            if (selectedVariants[collections[Playground.invselectedY]] > 0)
                            {
                                selectedVariants[collections[Playground.invselectedY]]--;
                            }
                            break;
                        case ConsoleKey.E:
                            selectedVariants[collections[Playground.invselectedY]]++;
                            break;
                    }
                }
                if (collections.ElementAtOrDefault(Playground.invselectedY) != null)
                {
                    if (selectedVariants.ContainsKey(collections[Playground.invselectedY]))
                    {
                        if (selectedVariants[collections[Playground.invselectedY]] > biggestVariant)
                        {
                            selectedVariants[collections[Playground.invselectedY]] = biggestVariant;
                        }
                    }
                }
            }
            Console.Clear();
        }
        static void loadFromFile(string level)
        {
            if (level.Substring(0, 6) == "string")
            {
                Playground.levelID = level.Substring(6);
                makeLevelfromString(Playground.generatorLevels[Int32.Parse(level.Substring(6))]);
            }
            else
            {
                if (!Directory.Exists(Playground.file))
                {
                    Directory.CreateDirectory(Playground.file);
                }
                if (!File.Exists(Playground.file + level))
                {
                    var client = new WebClient();
                    try
                    {
                        client.DownloadFile("http://preview.craftscripts.com/GatePlayground/Levels/" + level, Playground.file + level);
                    }
                    catch (System.Net.WebException)
                    {
                        File.Delete(Playground.file + level);
                    }
                }
                if (File.Exists(Playground.file + level))
                {
                    var xml = new XmlDocument();
                    xml.Load(Playground.file + level);
                    XmlNode xml2 = xml.SelectSingleNode("WORLD");
                    XmlNodeList nl = xml2.SelectNodes("WORLD");
                    XmlNode root = nl[0];
                    foreach (XmlNode node in root.ChildNodes)
                    {
                        foreach (XmlNode node2 in node.ChildNodes)
                        {
                            foreach (XmlNode node3 in node2.ChildNodes)
                            {
                                Console.Write(node.Name);
                                Console.Write(node2.Name);
                                Console.Write(node3.Name);
                                Console.Write(node3.InnerText);
                                Playground.world[Int32.Parse(node.Name.Substring(1))][Int32.Parse(node2.Name.Substring(1))][Int32.Parse(node3.Name.Substring(1))] = Int32.Parse(node3.InnerText);
                            }
                        }
                    }
                    if (xml2.SelectSingleNode("SIGNALS") != null)
                    {
                        XmlNodeList nl2 = xml2.SelectNodes("SIGNALS");
                        XmlNode root2 = nl2[0];
                        foreach (XmlNode node in root2.ChildNodes)
                        {
                            foreach (XmlNode node2 in node.ChildNodes)
                            {
                                foreach (XmlNode node3 in node2.ChildNodes)
                                {
                                    foreach (XmlNode node4 in node3.ChildNodes)
                                    {
                                        if (node4.InnerText != "0")
                                        {
                                            Console.Write("SIGNAL FOUND");
                                        }
                                        Playground.signals[Int32.Parse(node.Name.Substring(1))][Int32.Parse(node2.Name.Substring(1))][Int32.Parse(node3.Name.Substring(1))][Int32.Parse(node4.Name.Substring(1))] = Int32.Parse(node4.InnerText);
                                    }
                                }
                            }
                        }
                    }
                    if (xml2.SelectSingleNode("BLOCKS") != null)
                    {
                        Playground.enabledModules = new List<int>();
                        XmlNodeList nl2 = xml2.SelectNodes("BLOCKS");
                        XmlNode root2 = nl2[0];
                        foreach (XmlNode node in root2.ChildNodes)
                        {
                            if (Convert.ToBoolean(node.InnerText))
                            {
                                Playground.enabledModules.Add(Int32.Parse(node.Name.Substring(1)));
                            }
                        }
                    }
                }
            }
        }
        static void saveToFile(string name)
        {
            if (!Directory.Exists(Playground.file))
            {
                Directory.CreateDirectory(Playground.file);
            }
            XmlDocument xml = new XmlDocument();
            xml.LoadXml("<?xml version='1.0'?><WORLD></WORLD>");
            xml.DocumentElement.AppendChild(xml.CreateElement("WORLD"));
            xml.DocumentElement.AppendChild(xml.CreateElement("SIGNALS"));
            xml.DocumentElement.AppendChild(xml.CreateElement("BLOCKS"));
            for (int i1 = 0; i1 < Playground.layers; i1++)
            {
                xml.DocumentElement.SelectSingleNode("WORLD").AppendChild(xml.CreateElement("n" + i1));
                for (int i2 = 0; i2 < Playground.width; i2++)
                {
                    xml.DocumentElement.SelectSingleNode("WORLD").SelectSingleNode("n" + i1).AppendChild(xml.CreateElement("n" + i2));
                    for (int i3 = 0; i3 < Playground.height; i3++)
                    {
                        xml.DocumentElement.SelectSingleNode("WORLD").SelectSingleNode("n" + i1).SelectSingleNode("n" + i2).AppendChild(xml.CreateElement("n" + i3));
                        xml.DocumentElement.SelectSingleNode("WORLD").SelectSingleNode("n" + i1).SelectSingleNode("n" + i2).SelectSingleNode("n" + i3).InnerText = Playground.world[i1][i2][i3].ToString();
                    }
                }
            }
            for (int i1 = 0; i1 < Playground.layers; i1++)
            {
                xml.DocumentElement.SelectSingleNode("SIGNALS").AppendChild(xml.CreateElement("n" + i1));
                for (int i2 = 0; i2 < Playground.width; i2++)
                {
                    xml.DocumentElement.SelectSingleNode("SIGNALS").SelectSingleNode("n" + i1).AppendChild(xml.CreateElement("n" + i2));
                    for (int i3 = 0; i3 < Playground.height; i3++)
                    {
                        xml.DocumentElement.SelectSingleNode("SIGNALS").SelectSingleNode("n" + i1).SelectSingleNode("n" + i2).AppendChild(xml.CreateElement("n" + i3));
                        for (int i4 = 0; i4 < 7; i4++)
                        {
                            xml.DocumentElement.SelectSingleNode("SIGNALS").SelectSingleNode("n" + i1).SelectSingleNode("n" + i2).SelectSingleNode("n" + i3).AppendChild(xml.CreateElement("s" + i4));
                            xml.DocumentElement.SelectSingleNode("SIGNALS").SelectSingleNode("n" + i1).SelectSingleNode("n" + i2).SelectSingleNode("n" + i3).SelectSingleNode("s" + i4).InnerText = ((Playground.signals[i1][i2][i3][i4] + Playground.signalHold[i1][i2][i3][i4])).ToString();
                        }
                    }
                }
            }
            for (int i1 = 0; i1 < Playground.gates.Count; i1++)
            {
                xml.DocumentElement.SelectSingleNode("BLOCKS").AppendChild(xml.CreateElement("b" + i1));
                xml.DocumentElement.SelectSingleNode("BLOCKS").SelectSingleNode("b" + i1).InnerText = Playground.enabledModules.Contains(i1).ToString();

            }
            xml.Save(Playground.file + name);
        }
        public static void displayStrings(int layer)
        {
            int width = 3;
            foreach (string str in Playground.displayStrings)
            {
                string[] settings = str.Split(' ');
                if (Int32.Parse(settings[0]) == layer)
                {
                    Console.SetCursorPosition(Int32.Parse(settings[1]), Int32.Parse(settings[2]));
                    width = Int32.Parse(settings[3]);
                    Console.Write("{0," + width + "}", string.Join(" ", settings.Skip(4)));
                }
            }
        }
        public static void makeLevelfromString(string str)
        {
            Playground.levelInputs = new List<List<int>>();
            Playground.levelOutputs = new List<List<int>>();
            List<int> inputs = new List<int>();
            string[] options = str.Split(',');
            for (int i = 0; i < Playground.layers; i++)
            {
                for (int i2 = 0; i2 < Playground.width; i2++)
                {
                    Playground.world[i][i2][0] = 336;
                    Playground.world[i][i2][Playground.height - 1] = 338;
                    Playground.lockObj[i][i2][0] = 1;
                    Playground.lockObj[i][i2][Playground.height - 1] = 1;
                }
                for (int i2 = 0; i2 < Playground.height; i2++)
                {
                    Playground.world[i][0][i2] = 339;
                    Playground.world[i][Playground.width - 1][i2] = 337;
                    Playground.lockObj[i][0][i2] = 1;
                    Playground.lockObj[i][Playground.width - 1][i2] = 1;
                }
                Playground.world[i][0][0] = 340;
                Playground.world[i][Playground.width - 1][0] = 341;
                Playground.world[i][Playground.width - 1][Playground.height - 1] = 343;
                Playground.world[i][0][Playground.height - 1] = 342;
            }
            int maxWidth = Playground.width - 2;
            for (int j = 0; j < Int32.Parse(options[0].Split(';')[0]); j++)
            {
                int posX = maxWidth / (Int32.Parse(options[0].Split(';')[0]) + 1) * (j + 1);
                Playground.world[Playground.baseLayer][posX][0] = 344;
                Playground.levelInputs.Add(new List<int>()
                {
                    Playground.baseLayer,
                    posX,
                    0
                });
            }
            for (int j = 0; j < Int32.Parse(options[0].Split(';')[1]); j++)
            {
                int posX = maxWidth / (Int32.Parse(options[0].Split(';')[1]) + 1) * (j + 1);
                Playground.world[Playground.baseLayer][posX][Playground.height - 1] = 346;
                Playground.levelOutputs.Add(new List<int>()
                {
                    Playground.baseLayer,
                    posX,
                    Playground.height-1
                });
            }
            Playground.winConditions = options[1].Split(';').ToList();
            Playground.enabledModules = new List<int>();
            foreach (string group in options[2].Split(';'))
            {
                if (group == "*")
                {
                    foreach (KeyValuePair<string, Dictionary<string, int>> Sgroup in Playground.selector)
                    {
                        foreach (KeyValuePair<string, int> groupItems in Sgroup.Value)
                        {
                            Playground.enabledModules.Add(groupItems.Value);
                        }
                    }
                }
                else if (Int32.TryParse(group, out int result))
                {
                    if (result < 0)
                    {
                        Playground.enabledModules.Remove(Math.Abs(result));
                    }
                }
                else
                {
                    foreach (KeyValuePair<string, int> groupItems in Playground.selector[group])
                    {
                        Playground.enabledModules.Add(groupItems.Value);
                    }

                }
            }
            Playground.nextLvl = options[3];
        }
        static void initTest(int conditionID)
        {
            string condition = Playground.winConditions[conditionID];
            List<string> inputs = condition.Split('~')[0].Split('=')[0].Split('+').ToList();
            List<string> outputs = condition.Split('~')[0].Split('=')[1].Split('+').ToList();
            Playground.testTimestamp = DateTime.Now;
            Playground.testTimeout = Int32.Parse(condition.Split('~')[1]);
            foreach (string input in inputs)
            {
                string[] opt = input.Split('.');
                int[] inputPos = Playground.levelInputs[Int32.Parse(opt[0])].ToArray();
                if (opt[1] == "*")
                {
                    opt[1] = "1";
                }
                Playground.signals[inputPos[0]][inputPos[1]][inputPos[2]][6] = Int32.Parse(opt[1]) + 3;
            }
        }
        static bool checkTest(int conditionID)
        {
            bool won = true;
            string condition = Playground.winConditions[conditionID];
            List<string> inputs = condition.Split('~')[0].Split('=')[0].Split('+').ToList();
            List<string> outputs = condition.Split('~')[0].Split('=')[1].Split('+').ToList();
            foreach (string output in outputs)
            {
                string[] opt = output.Split('.');
                int[] outputPos = Playground.levelOutputs[Int32.Parse(opt[0])].ToArray();
                int signal = Playground.signals[outputPos[0]][outputPos[1]][outputPos[2]][1];
                if (opt[1] == "*")
                {
                    if (signal < 1)
                    {
                        won = false;
                    }
                }
                else if (opt[1] == "!")
                {
                    if (signal > 0)
                    {
                        won = false;
                    }
                }
                else
                {
                    int strength = Int32.Parse(opt[1]);
                    if (signal != strength + 3)
                    {
                        won = false;
                    }
                }
            }
            return won;
        }
        static void info_screen(bool doesntExist = false, int idToShow = -1)
        {
            int screen_size = Playground.width * (5 + Playground.Spacing) + 1 + 50;

            List<string> objectinfos = new List<string>()
            {
                "Pipe, A default Pipe that sends a signal with default signal strength to the other end.",
                "AND-Gate, Outputs a signal when both inputs are activated.",
                "OR-Gate, Outputs a signal when one or both of the inputs are activated.",
                "Split, Sends a signal from the input to both outputs.",
                "Turn Right, A default pipe that sends a signal with default signal strength to the other end.",
                "Turn Left, A default pipe that sends a signal with default signal strength to the other end.",
                "Signal Emitter, Emits a constant default signal.",
                "Lamp, Shows incoming signals.",
                "Timing Gate (1 Sec), Delays any incoming signal by 1 second.",
                "Timing Gate (5 Sec), Delays any incoming signal by 5 seconds.",
                "Timing Gate (30 Sec), Delays any incoming signal by 30 seconds.",
                "Button, Outputs a default signal when it is selected and SPACE is pressed.",
                "Timing Gate (1 Min), Delays any incoming signal by 1 minute.",
                "Timing Gate (5 Min), Delays any incoming signal by 5 minutes.",
                "NOT-Gate, Outputs a signal when no input is activated.",
                "TriSplit, Sends an incomming signal to three outputs.",
                "OR-Gate, Outputs a signal when one or both of the inputs are activated.",
                "OR-Gate, Outputs a signal when one or both of the inputs are activated.",
                "Elevator Up, A default pipe that sends a signal up one layer.",
                "Elevator Up, A default pipe that sends a signal up one layer.",
                "Vertical Pipe, A default pipe that extends a vertical signal.",
                "Elevator Down, A default pipe that sends a signal down one layer.",
                "Elevator Down, A default pipe that sends a signal down one layer.",
                "Timing Gate (10 Min), Delays any incoming signal by 10 minutes.",
                "Timing Gate (30 Min), Delays any incoming signal by 30 minutes.",
                "Timing Gate (1 Hour), Delays any incoming signal by 1 hour.",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "AND-Gate, Outputs a signal when both inputs are activated.",
                "AND-Gate, Outputs a signal when both inputs are activated.",
                "Split, Sends a signal from the input to both outputs.",
                "Split, Sends a signal from the input to both outputs.",
                "Beeper, Makes a Beep-Sound when any input is activated.",
                "",
                "Level Loader, Loads a new level when any input is activated.",
                "Data Cables, Sends a signal that contains a numeric value.",
                "AND-Gate, Outputs the lower value when both inputs are activated.",
                "OR-Gate, Outputs the higher value when one or both of the inputs are activated.",
                "Addition-Gate, Outputs the sum of both values when one or both of the inputs are activated.",
                "Split-Gate, Sends a signal from the input to both outputs.",
                "Turn Right, Sends a signal that contains a numeric value.",
                "Turn Left, Sends a signal that contains a numeric value.",
                "",
                "",
                "Display, Displays the numeric value of a data input.",
                "Key Reader, Outputs the numeric KeyCode of any pressed key.",
                "Check-Gate, Outputs the input values when they are identical.",
                "Data Emitter (1), Emits a constant datastream with a value of 1.",
                "Data Emitter (2), Emits a constant datastream with a value of 2.",
                "Data Emitter (4), Emits a constant datastream with a value of 4.",
                "Data Emitter (8), Emits a constant datastream with a value of 8.",
                "Data Emitter (16), Emits a constant datastream with a value of 16.",
                "Data Emitter (32), Emits a constant datastream with a value of 32.",
                "Data Emitter (64), Emits a constant datastream with a value of 64.",
                "Data Emitter (128), Emits a constant datastream with a value of 128.",
                "Data Emitter (256), Emits a constant datastream with a value of 256.",
                "Data Emitter (512), Emits a constant datastream with a value of 512.",
                "Elevator Up, A default pipe that sends a datastream up one layer.",
                "Elevator Up, A default pipe that sends a datastream up one layer.",
                "Vertical Pipe, A default pipe that extends a vertical datastream.",
                "Elevator Down, A default pipe that sends a datastream down one layer.",
                "Elevator Down, A default pipe that sends a datastream down one layer.",
                "Data Emitter (1024), Emits a constant datastream with a value of 1024.",
                "AND-Gate, Outputs the lower value when both inputs are activated.",
                "OR-Gate, Outputs the higher value when one or both of the inputs are activated.",
                "Addition-Gate, Outputs the sum of both values when one or both of the inputs are activated.",
                "Split-Gate, Sends a signal from the input to both outputs.",
                "AND-Gate, Outputs the lower value when both inputs are activated.",
                "OR-Gate, Outputs the higher value when one or both of the inputs are activated.",
                "Addition-Gate, Outputs the sum of both values when one or both of the inputs are activated.",
                "Split-Gate, Sends a signal from the input to both outputs.",
                "Check-Gate, Outputs the input values when they are identical.",
                "Check-Gate, Outputs the input values when they are identical.",
                "Display Segment, Gets lighted up when receiving an input signal.",
                "Flood-Gate, Sends the datastream from the data input through{ when a signal input is received from the signal input.",
                "Border, End of a level.",
                "Border, End of a level.",
                "Level Connection, Sends and receives signals from/to a level.",
                "TriSplit, Sends an incomming signal to three outputs.",
                "",
                "",
                "",
                "",
                "",
            };
            for (int f = 3; f < 26; f++)
            {
                Console.SetCursorPosition(screen_size, f);
                Console.Write(new String(' ', Console.BufferWidth - screen_size));
            }

            int id = 0;
            Console.SetCursorPosition(screen_size, 3);
            if (!doesntExist)
            {
                id = Playground.world[Playground.LoadedLayer][Playground.selectedX][Playground.selectedY];
            }
            else
            {
                if (idToShow == -1)
                {
                    id = Playground.selectedO;
                }
                else
                {
                    id = idToShow;
                }
            }
            if (id > 3)
            {
                int id2 = id / 4 - 1;
                Console.Write(objectinfos[id2].Split(',')[0]);
                string type = "Signal";

                string tmp2 = Playground.gates[id];
                if (objectinfos[id2] != "")
                {
                    if (tmp2.Split(',')[1] == "" || tmp2.Split(',')[1] == "=")
                    {
                        type = "None";
                    }
                    if (Playground.dataModules.Contains(id))
                    {
                        type = "Data";
                    }
                    foreach (string tmp in Playground.noDataSides)
                    {
                        if (tmp.Substring(0, id.ToString().Length) == id.ToString())
                        {
                            type = "Mixed";
                        }
                    }
                    Console.SetCursorPosition(screen_size, 5);
                    Console.Write("Type: " + type);



                    display(id, screen_size, 7, true);
                    Console.SetCursorPosition(screen_size, 13);
                    Console.Write("Inputs:");
                    Console.SetCursorPosition(screen_size, 14);
                    string[] oren = tmp2.Split(',')[1].Split('=')[0].Split(' ');
                    if (oren.Length == 3)
                    {
                        if (oren[2] != "A")
                        {
                            Console.Write(oren[2] + ":" + Playground.signals[Playground.LoadedLayer][Playground.selectedX][Playground.selectedY][Playground.sides[oren[2]]]);
                        }
                        if (oren[0] != "A")
                        {
                            Console.SetCursorPosition(screen_size, 15);
                            Console.Write(oren[0] + ":" + Playground.signals[Playground.LoadedLayer][Playground.selectedX][Playground.selectedY][Playground.sides[oren[0]]]);
                        }

                    }
                    else
                    {
                        if (oren[0] != "")
                        {
                            try
                            {
                                Console.Write(oren[0] + ":" + Playground.signals[Playground.LoadedLayer][Playground.selectedX][Playground.selectedY][Playground.sides[oren[0]]]);
                            }
                            catch (System.Collections.Generic.KeyNotFoundException) { }
                        }
                        else
                        {
                            Console.Write(oren[0] + ": No Signal ");
                        }
                    }

                    Console.SetCursorPosition(screen_size, 17);
                    Console.Write("Outputs:");
                    Console.SetCursorPosition(screen_size, 18);
                    if (tmp2.Split('=').Length == 2)
                    {
                        string[] outputs = tmp2.Split('=')[1].Split('+');
                        int number = 18;
                        foreach (string tmp3 in outputs)
                        {
                            if (tmp3.Substring(0, 1) != "A")
                            {
                                number++;
                                Console.Write(tmp3 + ":" + Playground.signals[Playground.LoadedLayer][Playground.selectedX][Playground.selectedY][Playground.sides[tmp3]]);
                                Console.SetCursorPosition(screen_size, number);
                            }
                        }
                    }
                    int row = 24;
                    if (objectinfos[id2].Split(',').Length == 2)
                    {
                        foreach (string txt in objectinfos[id2].Split(',')[1].Split('{'))
                        {
                            row++;
                            Console.SetCursorPosition(screen_size, row);
                            Console.Write(txt);
                        }
                    }
                }
            }
        }
    }
}