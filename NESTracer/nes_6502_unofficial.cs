using System;

namespace NESTracer
{
    internal partial class nes_6502
    {
        public bool g_cpu_halted;

        private void initialize_2A03()
        {
            // 2A03 accepts the NMOS 6502 undocumented opcodes.  These entries keep
            // games that rely on common illegal opcodes from crashing on a null func.
            set_op(0x1A, op_NOP, "NOP", "", ADDRESSING_TYPE.Implied, 1, 2);
            set_op(0x3A, op_NOP, "NOP", "", ADDRESSING_TYPE.Implied, 1, 2);
            set_op(0x5A, op_NOP, "NOP", "", ADDRESSING_TYPE.Implied, 1, 2);
            set_op(0x7A, op_NOP, "NOP", "", ADDRESSING_TYPE.Implied, 1, 2);
            set_op(0xDA, op_NOP, "NOP", "", ADDRESSING_TYPE.Implied, 1, 2);
            set_op(0xFA, op_NOP, "NOP", "", ADDRESSING_TYPE.Implied, 1, 2);

            set_op(0x80, op_NOP_imm, "NOP", "#VAL", ADDRESSING_TYPE.Immediate, 2, 2);
            set_op(0x82, op_NOP_imm, "NOP", "#VAL", ADDRESSING_TYPE.Immediate, 2, 2);
            set_op(0x89, op_NOP_imm, "NOP", "#VAL", ADDRESSING_TYPE.Immediate, 2, 2);
            set_op(0xC2, op_NOP_imm, "NOP", "#VAL", ADDRESSING_TYPE.Immediate, 2, 2);
            set_op(0xE2, op_NOP_imm, "NOP", "#VAL", ADDRESSING_TYPE.Immediate, 2, 2);
            set_op(0x04, op_NOP_read, "NOP", "VAL", ADDRESSING_TYPE.Zeropage, 2, 3);
            set_op(0x44, op_NOP_read, "NOP", "VAL", ADDRESSING_TYPE.Zeropage, 2, 3);
            set_op(0x64, op_NOP_read, "NOP", "VAL", ADDRESSING_TYPE.Zeropage, 2, 3);
            set_op(0x14, op_NOP_read, "NOP", "VAL,X", ADDRESSING_TYPE.ZeropageX, 2, 4);
            set_op(0x34, op_NOP_read, "NOP", "VAL,X", ADDRESSING_TYPE.ZeropageX, 2, 4);
            set_op(0x54, op_NOP_read, "NOP", "VAL,X", ADDRESSING_TYPE.ZeropageX, 2, 4);
            set_op(0x74, op_NOP_read, "NOP", "VAL,X", ADDRESSING_TYPE.ZeropageX, 2, 4);
            set_op(0xD4, op_NOP_read, "NOP", "VAL,X", ADDRESSING_TYPE.ZeropageX, 2, 4);
            set_op(0xF4, op_NOP_read, "NOP", "VAL,X", ADDRESSING_TYPE.ZeropageX, 2, 4);
            set_op(0x0C, op_NOP_read, "NOP", "VAL", ADDRESSING_TYPE.Absolute, 3, 4);
            set_op(0x1C, op_NOP_read, "NOP", "VAL,X", ADDRESSING_TYPE.AbsoluteX, 3, 4, true);
            set_op(0x3C, op_NOP_read, "NOP", "VAL,X", ADDRESSING_TYPE.AbsoluteX, 3, 4, true);
            set_op(0x5C, op_NOP_read, "NOP", "VAL,X", ADDRESSING_TYPE.AbsoluteX, 3, 4, true);
            set_op(0x7C, op_NOP_read, "NOP", "VAL,X", ADDRESSING_TYPE.AbsoluteX, 3, 4, true);
            set_op(0xDC, op_NOP_read, "NOP", "VAL,X", ADDRESSING_TYPE.AbsoluteX, 3, 4, true);
            set_op(0xFC, op_NOP_read, "NOP", "VAL,X", ADDRESSING_TYPE.AbsoluteX, 3, 4, true);

            register_rmw_group(op_SLO, "SLO", 0x03, 0x07, 0x0F, 0x13, 0x17, 0x1B, 0x1F);
            register_rmw_group(op_RLA, "RLA", 0x23, 0x27, 0x2F, 0x33, 0x37, 0x3B, 0x3F);
            register_rmw_group(op_SRE, "SRE", 0x43, 0x47, 0x4F, 0x53, 0x57, 0x5B, 0x5F);
            register_rmw_group(op_RRA, "RRA", 0x63, 0x67, 0x6F, 0x73, 0x77, 0x7B, 0x7F);
            register_rmw_group(op_DCP, "DCP", 0xC3, 0xC7, 0xCF, 0xD3, 0xD7, 0xDB, 0xDF);
            register_rmw_group(op_ISC, "ISC", 0xE3, 0xE7, 0xEF, 0xF3, 0xF7, 0xFB, 0xFF);

            set_op(0x87, op_SAX, "SAX", "VAL", ADDRESSING_TYPE.Zeropage, 2, 3);
            set_op(0x97, op_SAX, "SAX", "VAL,Y", ADDRESSING_TYPE.ZeropageY, 2, 4);
            set_op(0x83, op_SAX, "SAX", "(VAL,X)", ADDRESSING_TYPE.IndirectX, 2, 6);
            set_op(0x8F, op_SAX, "SAX", "VAL", ADDRESSING_TYPE.Absolute, 3, 4);

            set_op(0xA7, op_LAX, "LAX", "VAL", ADDRESSING_TYPE.Zeropage, 2, 3);
            set_op(0xB7, op_LAX, "LAX", "VAL,Y", ADDRESSING_TYPE.ZeropageY, 2, 4);
            set_op(0xA3, op_LAX, "LAX", "(VAL,X)", ADDRESSING_TYPE.IndirectX, 2, 6);
            set_op(0xB3, op_LAX, "LAX", "(VAL),Y", ADDRESSING_TYPE.IndirectY, 2, 5, true);
            set_op(0xAF, op_LAX, "LAX", "VAL", ADDRESSING_TYPE.Absolute, 3, 4);
            set_op(0xBF, op_LAX, "LAX", "VAL,Y", ADDRESSING_TYPE.AbsoluteY, 3, 4, true);
            set_op(0xAB, op_LAX, "LAX", "#VAL", ADDRESSING_TYPE.Immediate, 2, 2);

            set_op(0x0B, op_ANC, "ANC", "#VAL", ADDRESSING_TYPE.Immediate, 2, 2);
            set_op(0x2B, op_ANC, "ANC", "#VAL", ADDRESSING_TYPE.Immediate, 2, 2);
            set_op(0x4B, op_ALR, "ALR", "#VAL", ADDRESSING_TYPE.Immediate, 2, 2);
            set_op(0x6B, op_ARR, "ARR", "#VAL", ADDRESSING_TYPE.Immediate, 2, 2);
            set_op(0x8B, op_XAA, "XAA", "#VAL", ADDRESSING_TYPE.Immediate, 2, 2);
            set_op(0xCB, op_AXS, "AXS", "#VAL", ADDRESSING_TYPE.Immediate, 2, 2);
            set_op(0xEB, op_SBC, "SBC", "#VAL", ADDRESSING_TYPE.Immediate, 2, 2);

            set_op(0x93, op_AHX, "AHX", "(VAL),Y", ADDRESSING_TYPE.IndirectY, 2, 6);
            set_op(0x9F, op_AHX, "AHX", "VAL,Y", ADDRESSING_TYPE.AbsoluteY, 3, 5);
            set_op(0x9B, op_TAS, "TAS", "VAL,Y", ADDRESSING_TYPE.AbsoluteY, 3, 5);
            set_op(0x9C, op_SHY, "SHY", "VAL,X", ADDRESSING_TYPE.AbsoluteX, 3, 5);
            set_op(0x9E, op_SHX, "SHX", "VAL,Y", ADDRESSING_TYPE.AbsoluteY, 3, 5);
            set_op(0xBB, op_LAS, "LAS", "VAL,Y", ADDRESSING_TYPE.AbsoluteY, 3, 4, true);

            int[] jamOpcodes = { 0x02, 0x12, 0x22, 0x32, 0x42, 0x52, 0x62, 0x72, 0x92, 0xB2, 0xD2, 0xF2 };
            foreach (int opcode in jamOpcodes)
            {
                set_op(opcode, op_KIL, "KIL", "", ADDRESSING_TYPE.Implied, 1, 2);
            }

            for (int i = 0; i < g_oplist.Length; i++)
            {
                if (g_oplist[i].func == null)
                {
                    set_op(i, op_KIL, "KIL", "", ADDRESSING_TYPE.Implied, 1, 2);
                }
            }
        }

        private void set_op(int opcode, Action func, string name, string format, ADDRESSING_TYPE addr, ushort size, int clock, bool pageBoundary = false)
        {
            g_oplist[opcode] = new OPLIST()
            {
                func = func,
                opname_out = name,
                format = format,
                addr = addr,
                size = size,
                clock = clock,
                page_boundary = pageBoundary
            };
        }

        private void register_rmw_group(Action func, string name, int indirectX, int zeropage, int absolute, int indirectY, int zeropageX, int absoluteY, int absoluteX)
        {
            set_op(indirectX, func, name, "(VAL,X)", ADDRESSING_TYPE.IndirectX, 2, 8);
            set_op(zeropage, func, name, "VAL", ADDRESSING_TYPE.Zeropage, 2, 5);
            set_op(absolute, func, name, "VAL", ADDRESSING_TYPE.Absolute, 3, 6);
            set_op(indirectY, func, name, "(VAL),Y", ADDRESSING_TYPE.IndirectY, 2, 8);
            set_op(zeropageX, func, name, "VAL,X", ADDRESSING_TYPE.ZeropageX, 2, 6);
            set_op(absoluteY, func, name, "VAL,Y", ADDRESSING_TYPE.AbsoluteY, 3, 7);
            set_op(absoluteX, func, name, "VAL,X", ADDRESSING_TYPE.AbsoluteX, 3, 7);
        }

        private void compare_A(byte in_val)
        {
            int w_val2 = (int)g_reg_A - (int)in_val;
            C_check_sub(w_val2);
            NZ_check((byte)w_val2);
        }

        private void op_NOP_imm()
        {
        }

        private void op_NOP_read()
        {
            addressing_read();
        }

        private void op_KIL()
        {
            g_cpu_halted = true;
            g_reg_PC -= 1;
        }

        private void op_SLO()
        {
            byte w_val = addressing_read();
            g_flag_C = (w_val & 0x80) == 0x80;
            w_val = (byte)(w_val << 1);
            addressing_write(w_val);
            g_reg_A |= w_val;
            NZ_check(g_reg_A);
        }

        private void op_RLA()
        {
            byte w_val = addressing_read();
            bool old_c = g_flag_C;
            g_flag_C = (w_val & 0x80) == 0x80;
            w_val = (byte)(w_val << 1);
            if (old_c == true) w_val |= 0x01;
            addressing_write(w_val);
            g_reg_A &= w_val;
            NZ_check(g_reg_A);
        }

        private void op_SRE()
        {
            byte w_val = addressing_read();
            g_flag_C = (w_val & 0x01) == 0x01;
            w_val = (byte)(w_val >> 1);
            addressing_write(w_val);
            g_reg_A ^= w_val;
            NZ_check(g_reg_A);
        }

        private void op_RRA()
        {
            byte w_val = addressing_read();
            bool old_c = g_flag_C;
            g_flag_C = (w_val & 0x01) == 0x01;
            w_val = (byte)(w_val >> 1);
            if (old_c == true) w_val |= 0x80;
            addressing_write(w_val);
            op_ADC_SUB(w_val);
        }

        private void op_DCP()
        {
            byte w_val = addressing_read();
            w_val -= 1;
            addressing_write(w_val);
            compare_A(w_val);
        }

        private void op_ISC()
        {
            byte w_val = addressing_read();
            w_val += 1;
            addressing_write(w_val);
            op_ADC_SUB((byte)(w_val ^ 0xff));
        }

        private void op_SAX()
        {
            addressing_write((byte)(g_reg_A & g_reg_X));
        }

        private void op_LAX()
        {
            byte w_val = addressing_read();
            g_reg_A = w_val;
            g_reg_X = w_val;
            NZ_check(w_val);
        }

        private void op_ANC()
        {
            g_reg_A &= addressing_read();
            NZ_check(g_reg_A);
            g_flag_C = g_flag_N;
        }

        private void op_ALR()
        {
            g_reg_A &= addressing_read();
            g_flag_C = (g_reg_A & 0x01) == 0x01;
            g_reg_A = (byte)(g_reg_A >> 1);
            NZ_check(g_reg_A);
        }

        private void op_ARR()
        {
            g_reg_A &= addressing_read();
            g_reg_A = (byte)((g_reg_A >> 1) | (g_flag_C ? 0x80 : 0x00));
            NZ_check(g_reg_A);
            g_flag_C = (g_reg_A & 0x40) == 0x40;
            g_flag_V = (((g_reg_A >> 6) ^ (g_reg_A >> 5)) & 0x01) == 0x01;
        }

        private void op_XAA()
        {
            // XAA is unstable on real NMOS silicon.  This approximation matches the
            // commonly used emulator behavior well enough for ROMs that probe it.
            g_reg_A = (byte)((g_reg_A | 0xee) & g_reg_X & addressing_read());
            NZ_check(g_reg_A);
        }

        private void op_AXS()
        {
            byte w_val = addressing_read();
            int w_result = (g_reg_A & g_reg_X) - w_val;
            g_flag_C = w_result >= 0;
            g_reg_X = (byte)w_result;
            NZ_check(g_reg_X);
        }

        private void op_AHX()
        {
            ushort w_addr = addressing_address();
            byte w_val = (byte)(g_reg_A & g_reg_X & (((w_addr >> 8) + 1) & 0xff));
            nes_main.g_nes_bus.write1(w_addr, w_val);
        }

        private void op_TAS()
        {
            ushort w_addr = addressing_address();
            g_reg_S = (byte)(g_reg_A & g_reg_X);
            byte w_val = (byte)(g_reg_S & (((w_addr >> 8) + 1) & 0xff));
            nes_main.g_nes_bus.write1(w_addr, w_val);
        }

        private void op_SHY()
        {
            ushort w_addr = addressing_address();
            byte w_val = (byte)(g_reg_Y & (((w_addr >> 8) + 1) & 0xff));
            nes_main.g_nes_bus.write1(w_addr, w_val);
        }

        private void op_SHX()
        {
            ushort w_addr = addressing_address();
            byte w_val = (byte)(g_reg_X & (((w_addr >> 8) + 1) & 0xff));
            nes_main.g_nes_bus.write1(w_addr, w_val);
        }

        private void op_LAS()
        {
            byte w_val = (byte)(addressing_read() & g_reg_S);
            g_reg_A = w_val;
            g_reg_X = w_val;
            g_reg_S = w_val;
            NZ_check(w_val);
        }
    }
}
