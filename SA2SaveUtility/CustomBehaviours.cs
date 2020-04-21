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
                        if (minimum <= number && maximum >= number) {
                            Debug.WriteLine(((NumericUpDown)c).Name + " cannot be set to " + value + " it should be between " + ((NumericUpDown)c).Minimum + " and " + ((NumericUpDown)c).Maximum + ". This might be an \"endian\" issue, setting the value to " + number);
                            ((NumericUpDown)c).Value = number;
                        } else {
                            Debug.WriteLine(((NumericUpDown)c).Name + " cannot be set to " + value + " or " + number + ", it should be between " + ((NumericUpDown)c).Minimum + " and " + ((NumericUpDown)c).Maximum);
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
                    Debug.WriteLine("Index is being set too high for item " + c.Name + " (" + index + " should be less than " + c.Items.Count + "), setting it to the last item");
                    c.SelectedIndex = c.Items.Count - 1;
                } else if (index < 0) {
                    Debug.WriteLine("Index is being set too low for item " + c.Name + " (" + index + " should be greater than 0), setting it to the first item");
                    c.SelectedIndex = 0;
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
