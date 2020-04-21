using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace SA2SaveUtility {
    public static class CustomBehaviours {
        public static void Value(this Control c, int value) {
            bool denyChange = false;
            if (Main.isRTE && Main.rteUpdates) { denyChange = c.Focused; }
            if (c is TrackBar) {
                if (((TrackBar)c).Value != value && !denyChange) { ((TrackBar)c).Value = value; }
            }
            if (c is NumericUpDown) {
                decimal minimum = ((NumericUpDown)c).Minimum;
                decimal maximum = ((NumericUpDown)c).Maximum;
                if (((NumericUpDown)c).Value != value && !denyChange) {
                    if (minimum <= value && maximum >= value) {
                        ((NumericUpDown)c).Value = value;
                    } else {
                        int number = BitConverter.ToInt32(BitConverter.GetBytes(value).Reverse().ToArray(), 0);
                        Debug.WriteLine(((NumericUpDown)c).Name + " cannot be set to " + value + " it should be between " + ((NumericUpDown)c).Minimum + " and " + ((NumericUpDown)c).Maximum);
                        if (minimum <= number && maximum >= number) {
                            Debug.WriteLine("This might be an \"endian\" issue, setting the value to " + number);
                            ((NumericUpDown)c).Value = number;
                        } else {
                            Debug.WriteLine("Cannot set the value to " + number);
                        }
                    }
                }
            }
        }

        public static void SelectedIndex(this ComboBox c, int index) {
            bool denyChange = false;
            if (Main.isRTE && Main.rteUpdates) { denyChange = c.Focused; }
            if (c.SelectedIndex != index && !denyChange) {
                if (index > c.Items.Count) {
                    Debug.WriteLine("Index " + index + " is too high (should be less than " + c.Items.Count + "), setting it to the last item - " + c.Name);
                    c.SelectedIndex = c.Items.Count;
                } else {
                    c.SelectedIndex = index;
                }
            }
        }

        public static void Text(this TextBox c, string text) {
            bool denyChange = false;
            if (Main.isRTE && Main.rteUpdates) { denyChange = c.Focused; }
            if (c.Text != text && !denyChange) { c.Text = text; }
        }

        public static void Checked(this CheckBox c, bool _checked) {
            bool denyChange = false;
            if (Main.isRTE && Main.rteUpdates) { denyChange = c.Focused; }
            if (c.Checked != _checked && !denyChange) { c.Checked = _checked; }
        }
    }
}
