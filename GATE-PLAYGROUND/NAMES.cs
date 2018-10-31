using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GATE_PLAYGROUND
{
    class NAMES
    {
        public class VOID
        {
            public static int VOID1 = 0;
            public static int VOID2 = 1;
            public static int VOID3 = 2;
            public static int VOID4 = 3;
            public class MULTIPLES
            {
                public static int NONE = -1;
            }
        }
        public class DEFAULT_PIPES
        {
            public static int STRAIGHT_N_S = 4;
            public static int STRAIGHT_W_E = 5;
            public static int STRAIGHT_S_N = 6;
            public static int STRAIGHT_E_W = 7;

            public static int TURN_W_S = 20;
            public static int TURN_N_W = 21;
            public static int TURN_E_N = 22;
            public static int TURN_S_E = 23;

            public static int TURN_E_S = 24;
            public static int TURN_S_W = 25;
            public static int TURN_W_N = 26;
            public static int TURN_N_E = 27;
            public class MULTIPLES
            {
                public static int NONE = -1;
            }
        }
        public class LOGIC
        {

            public static int AND_WE_S = 8;
            public static int AND_NS_W = 9;
            public static int AND_WE_N = 10;
            public static int AND_NS_E = 11;

            public static int AND_WS_E = 148;
            public static int AND_NW_S = 149;
            public static int AND_EN_W = 150;
            public static int AND_SE_N = 151;

            public static int AND_ES_W = 152;
            public static int AND_SW_N = 153;
            public static int AND_WN_E = 154;
            public static int AND_NE_S = 155;

            public static int OR_WE_S = 12;
            public static int OR_NS_W = 13;
            public static int OR_WE_N = 14;
            public static int OR_NS_E = 15;

            public static int OR_EN_S = 68;
            public static int OR_SE_W = 69;
            public static int OR_WS_N = 70;
            public static int OR_NW_E = 71;

            public static int OR_SE_N = 72;
            public static int OR_WS_E = 73;
            public static int OR_NW_S = 74;
            public static int OR_EN_W = 75;

            public static int SPLIT_S_WE = 16;
            public static int SPLIT_W_NS = 17;
            public static int SPLIT_N_WE = 18;
            public static int SPLIT_E_NS = 19;

            public static int SPLIT_E_WS = 156;
            public static int SPLIT_S_NW = 157;
            public static int SPLIT_W_EN = 158;
            public static int SPLIT_N_SE = 159;

            public static int SPLIT_W_ES = 160;
            public static int SPLIT_N_WS = 161;
            public static int SPLIT_E_WN = 162;
            public static int SPLIT_S_NE = 163;

            public static int SPLIT_N_ESW = 64;
            public static int SPLIT_E_SWN = 65;
            public static int SPLIT_S_WNE = 66;
            public static int SPLIT_W_NES = 67;

            public static int NOT_N_S = 60;
            public static int NOT_W_E = 61;
            public static int NOT_S_N = 62;
            public static int NOT_E_W = 63;
            public class MULTIPLES
            {
                public static int NONE = -1;
            }
        }
        public class I_O
        {
            public static int EMITTER_S = 28;
            public static int EMITTER_W = 29;
            public static int EMITTER_N = 30;
            public static int EMITTER_E = 31;

            public static int LAMP_S = 32;
            public static int LAMP_W = 33;
            public static int LAMP_N = 34;
            public static int LAMP_E = 35;

            public static int BUTTON_S = 48;
            public static int BUTTON_W = 49;
            public static int BUTTON_N = 50;
            public static int BUTTON_E = 51;
            
            public class KEYREAD_OLD
            {

                public static int BIT1_S = 116;
                public static int BIT1_W = 117;
                public static int BIT1_N = 118;
                public static int BIT1_E = 119;

                public static int BIT2_S = 120;
                public static int BIT2_W = 121;
                public static int BIT2_N = 122;
                public static int BIT2_E = 123;

                public static int BIT3_S = 124;
                public static int BIT3_W = 125;
                public static int BIT3_N = 126;
                public static int BIT3_E = 127;

                public static int BIT4_S = 128;
                public static int BIT4_W = 129;
                public static int BIT4_N = 130;
                public static int BIT4_E = 131;

                public static int BIT5_S = 132;
                public static int BIT5_W = 133;
                public static int BIT5_N = 134;
                public static int BIT5_E = 135;

                public static int BIT6_S = 136;
                public static int BIT6_W = 137;
                public static int BIT6_N = 138;
                public static int BIT6_E = 139;

                public static int BIT7_S = 140;
                public static int BIT7_W = 141;
                public static int BIT7_N = 142;
                public static int BIT7_E = 143;

                public static int BIT8_S = 144;
                public static int BIT8_W = 145;
                public static int BIT8_N = 146;
                public static int BIT8_E = 147;
            }

            public static int BEEPER_S = 164;
            public static int BEEPER_W = 165;
            public static int BEEPER_N = 166;
            public static int BEEPER_E = 167;

            public static int KEYREAD_S = 216;
            public static int KEYREAD_W = 217;
            public static int KEYREAD_N = 218;
            public static int KEYREAD_E = 219;

            public static int DISPLAY = 328;

            public class MULTIPLES
            {
                public static int DISPLAY_A = 329;
                public static int DISPLAY_B = 330;
                public static int DISPLAY_C = 331;
            }
        }
        public class TIMING
        {

            public static int S1_N_S = 36; 
            public static int S1_E_W = 37; 
            public static int S1_S_N = 38; 
            public static int S1_W_E = 39; 

            public static int S5_N_S = 40; 
            public static int S5_E_W = 41; 
            public static int S5_S_N = 42; 
            public static int S5_W_E = 43; 

            public static int S30_N_S = 44; 
            public static int S30_E_W = 45; 
            public static int S30_S_N = 46; 
            public static int S30_W_E = 47; 

            public static int S60_N_S = 52; 
            public static int S60_E_W = 53; 
            public static int S60_S_N = 54; 
            public static int S60_W_E = 55; 

            public static int S300_N_S = 56; 
            public static int S300_E_W = 57; 
            public static int S300_S_N = 58; 
            public static int S300_W_E = 59; 

            public static int S600_N_S = 96; 
            public static int S600_E_W = 97; 
            public static int S600_S_N = 98; 
            public static int S600_W_E = 99; 

            public static int S1800_N_S = 100; 
            public static int S1800_E_W = 101; 
            public static int S1800_S_N = 102; 
            public static int S1800_W_E = 103; 

            public static int S3600_N_S = 104; 
            public static int S3600_E_W = 105; 
            public static int S3600_S_N = 106; 
            public static int S3600_W_E = 107;
            public class MULTIPLES
            {
                public static int NONE = -1;
            }
        }
        public class LAYERS
        {
            public static int N_T = 76;
            public static int E_T = 77;
            public static int S_T = 78;
            public static int W_T = 79;

            public static int N_B = 80;
            public static int E_B = 81;
            public static int S_B = 82;
            public static int W_B = 83;

            public static int B_T = 84;
            public static int T_B = 85;

            public static int T_N = 88;
            public static int T_E = 89;
            public static int T_S = 90;
            public static int T_W = 91;

            public static int B_N = 92;
            public static int B_E = 93;
            public static int B_S = 94;
            public static int B_w = 95;

            public class MULTIPLES
            {
                public static int B_T = 86;
                public static int T_B = 87;
            }
        }
        public class SYSTEM
        {
            public static int TEXT_MENU_WELCOME = 108;
            public static int TEXT_PLAYGROUND_WELCOME = 112;

            public static int NO_TEXTURE_OVERRIDE = 109;
            public static int NO_TEXTURE_OVERRIDE_CENTER = 110;
            public static int NO_TEXTURE_OVERRIDE_TOPLEFT = 111;

            public static int TEXT_START = 168;
            public static int TEXT_PLAYGROUND = 169;

            public static int LOADWORLD_PLAYGROUND = 172;
            public static int LOADWORLD_STRING0 = 173;

            public class MULTIPLES
            {
                public static int VOID_A = 170;
                public static int VOID_B = 171;

                public static int NO_TEXTURE_OVERRIDE_A = 113;
                public static int NO_TEXTURE_OVERRIDE_B = 114;
                public static int NO_TEXTURE_OVERRIDE_C = 115;
                public static int NO_TEXTURE_OVERRIDE_D = 174;
                public static int NO_TEXTURE_OVERRIDE_E = 175;
            }

            public class LEVEL
            {
                public class BORDER
                {
                    public static int WALL_N = 336;
                    public static int WALL_W = 337;
                    public static int WALL_S = 338;
                    public static int WALL_E = 339;

                    public static int CORNER_NW = 340;
                    public static int CORNER_NE = 341;
                    public static int CORNER_SW = 342;
                    public static int CORNER_SE = 343;
                    public class MULTIPLES
                    {
                        public static int NONE = -1;
                    }
                }

                public static int INPUT = 344;
                public static int OUTPUT = 346;

                public class MULTIPLES
                {
                    public static int INPUT = 345;
                    public static int OUTPUT = 347;
                }
            }

            public class TUTORIAL_TEXTS
            {
                public static int SIGNAL_INPUT = 352;
                public static int SIGNAL_OUTPUT = 353;
                public static int DATA_INPUT = 354;
                public static int DATA_OUTPUT = 355;
                public static int ACTIVE_CONNECTION = 356;

                public static int CONTROLS = 357;

                public class MULTIPLES
                {
                    public static int ACTIVE_CONNECTION_A = 358;
                    public static int ACTIVE_CONNECTION_B = 359;
                }
                public class LAYERLIST
                {
                    public class SELECTED_0
                    {
                        public static int TOP = 360;
                        public static int BOTTOM = 370;
                        public class MULTIPLES
                        {
                            public static int NONE = -1;
                        }
                    }
                    public class SELECTED_1
                    {
                        public static int TOP = 361;
                        public static int BOTTOM = 369;
                        public class MULTIPLES
                        {
                            public static int NONE = -1;
                        }
                    }
                    public class SELECTED_2
                    {
                        public static int TOP = 362;
                        public static int BOTTOM = 368;
                        public class MULTIPLES
                        {
                            public static int NONE = -1;
                        }
                    }
                    public class SELECTED_3
                    {
                        public static int TOP = 363;
                        public static int BOTTOM = 367;
                        public class MULTIPLES
                        {
                            public static int NONE = -1;
                        }
                    }
                    public class SELECTED_4
                    {
                        public static int BOTTOM = 366;
                        public class MULTIPLES
                        {
                            public static int NONE = -1;
                        }
                    }
                    public class SELECTED_5
                    {
                        public static int BOTTOM = 365;
                        public class MULTIPLES
                        {
                            public static int NONE = -1;
                        }
                    }
                    public class SELECTED_6
                    {
                        public static int BOTTOM = 364;
                        public class MULTIPLES
                        {
                            public static int NONE = -1;
                        }
                    }
                    public class MULTIPLES
                    {
                        public static int NONE = -1;
                    }
                }
            }
        }
        public class DATA
        {
            public class PIPES
            {
                public static int STRAIGHT_N_S = 176;
                public static int STRAIGHT_W_E = 177;
                public static int STRAIGHT_S_N = 178;
                public static int STRAIGHT_E_W = 179;

                public static int TURN_W_S = 196;
                public static int TURN_N_W = 197;
                public static int TURN_E_N = 198;
                public static int TURN_S_E = 199;

                public static int TURN_E_S = 200;
                public static int TURN_S_W = 201;
                public static int TURN_W_N = 202;
                public static int TURN_N_E = 203;
                public class MULTIPLES
                {
                    public static int NONE = -1;
                }
            }
            public class LOGIC
            {

                public static int AND_WE_S = 180;
                public static int AND_NS_W = 181;
                public static int AND_WE_N = 182;
                public static int AND_NS_E = 183;

                public static int AND_WS_E = 288;
                public static int AND_NW_S = 305;
                public static int AND_EN_W = 290;
                public static int AND_SE_N = 307;

                public static int AND_ES_W = 304;
                public static int AND_SW_N = 289;
                public static int AND_WN_E = 306;
                public static int AND_NE_S = 291;

                public static int OR_WE_S = 184;
                public static int OR_NS_W = 185;
                public static int OR_WE_N = 186;
                public static int OR_NS_E = 187;

                public static int OR_EN_S = 295;
                public static int OR_SE_W = 308;
                public static int OR_WS_N = 293;
                public static int OR_NW_E = 310;

                public static int OR_SE_N = 311;
                public static int OR_WS_E = 292;
                public static int OR_NW_S = 309;
                public static int OR_EN_W = 294;

                public static int SPLIT_S_WE = 192;
                public static int SPLIT_W_NS = 193;
                public static int SPLIT_N_WE = 194;
                public static int SPLIT_E_NS = 195;

                public static int SPLIT_E_WS = 300;
                public static int SPLIT_S_NW = 317;
                public static int SPLIT_W_EN = 302;
                public static int SPLIT_N_SE = 319;

                public static int SPLIT_W_ES = 316;
                public static int SPLIT_N_WS = 301;
                public static int SPLIT_E_WN = 318;
                public static int SPLIT_S_NE = 303;

                public static int SPLIT_N_ESW = 348;
                public static int SPLIT_E_SWN = 349;
                public static int SPLIT_S_WNE = 350;
                public static int SPLIT_W_NES = 351;

                public static int ADD_WE_S = 188;
                public static int ADD_NS_W = 189;
                public static int ADD_WE_N = 190;
                public static int ADD_NS_E = 191;

                public static int ADD_WS_E = 296;
                public static int ADD_WS_N = 297;
                public static int ADD_NE_W = 298;
                public static int ADD_NE_S = 299;

                public static int ADD_SE_W = 312;
                public static int ADD_NW_S = 313;
                public static int ADD_WN_E = 314;
                public static int ADD_ES_N = 315;

                public static int CHECK_WE_S = 220;
                public static int CHECK_NS_W = 221;
                public static int CHECK_WE_N = 222;
                public static int CHECK_NS_E = 223;

                public static int CHECK_WS_E = 320;
                public static int CHECK_WS_N = 321;
                public static int CHECK_NE_W = 322;
                public static int CHECK_NE_S = 323;

                public static int CHECK_SE_W = 324;
                public static int CHECK_NW_S = 325;
                public static int CHECK_WN_E = 326;
                public static int CHECK_ES_N = 327;

                public static int FLOOD_SE_W = 332;
                public static int FLOOD_WN_S = 333;
                public static int FLOOD_NW_E = 334;
                public static int FLOOD_ES_N = 335;
                public class MULTIPLES
                {
                    public static int NONE = -1;
                }
            }
            public class EMITTERS
            {
                public static int TEST_BIG_S = 204;
                public static int TEST_BIG_W = 205;
                public static int TEST_BIG_N = 206;
                public static int TEST_BIG_E = 207;

                public static int TEST_SMALL_S = 208;
                public static int TEST_SMALL_W = 209;
                public static int TEST_SMALL_N = 210;
                public static int TEST_SMALL_E = 211;

                public static int E1_S = 224;
                public static int E1_W = 225;
                public static int E1_N = 226;
                public static int E1_E = 227;

                public static int E2_S = 228;
                public static int E2_W = 229;
                public static int E2_N = 230;
                public static int E2_E = 231;

                public static int E4_S = 232;
                public static int E4_W = 233;
                public static int E4_N = 234;
                public static int E4_E = 235;

                public static int E8_S = 236;
                public static int E8_W = 237;
                public static int E8_N = 238;
                public static int E8_E = 239;

                public static int E16_S = 240;
                public static int E16_W = 241;
                public static int E16_N = 242;
                public static int E16_E = 243;

                public static int E32_S = 244;
                public static int E32_W = 245;
                public static int E32_N = 246;
                public static int E32_E = 247;

                public static int E64_S = 248;
                public static int E64_W = 249;
                public static int E64_N = 250;
                public static int E64_E = 251;

                public static int E128_S = 252;
                public static int E128_W = 253;
                public static int E128_N = 254;
                public static int E128_E = 255;

                public static int E256_S = 256;
                public static int E256_W = 257;
                public static int E256_N = 258;
                public static int E256_E = 259;

                public static int E512_S = 260;
                public static int E512_W = 261;
                public static int E512_N = 262;
                public static int E512_E = 263;

                public static int E1024_S = 284;
                public static int E1024_W = 285;
                public static int E1024_N = 286;
                public static int E1024_E = 287;
                public class MULTIPLES
                {
                    public static int NONE = -1;
                }
            }

            public static int DISPLAY_N_S = 268;
            public static int DISPLAY_E_W = 269;
            public static int DISPLAY_S_N = 270;
            public static int DISPLAY_W_E = 271;
            public class LAYERS
            {
                public static int N_T = 264;
                public static int E_T = 265;
                public static int S_T = 266;
                public static int W_T = 267;

                public static int N_B = 268;
                public static int E_B = 269;
                public static int S_B = 270;
                public static int W_B = 271;

                public static int B_T = 272;
                public static int T_B = 273;

                /// <summary>
                /// Identical to B_T
                /// </summary>
                public static int b_t = 274;

                /// <summary>
                /// Identical to T_B
                /// </summary>
                public static int t_b = 275;

                public static int T_N = 276;
                public static int T_E = 277;
                public static int T_S = 278;
                public static int T_W = 279;

                public static int B_N = 280;
                public static int B_E = 281;
                public static int B_S = 282;
                public static int B_w = 283;
                public class MULTIPLES
                {
                    public static int NONE = -1;
                }
            }
            public class MULTIPLES
            {
                public static int NONE = -1;
            }
        }
    }
}
