﻿using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using HueLib2;

namespace WinHue3
{
    /// <summary>
    /// Interaction logic for Form_HueTapConfig.xaml
    /// </summary>
    public partial class Form_HueTapConfig : Window
    {
        private Bridge _bridge;
        private int button;
        private readonly Color selectedColor = Color.FromArgb(128, 255, 0, 0);
        private readonly Color deselectedColor = Color.FromArgb(0, 0, 0, 0);
        private string _sensorid;
        public Form_HueTapConfig(Bridge br,string sensorid)
        {
            InitializeComponent();
            _bridge = br;
            Dictionary<string, Scene> scenelist;
            CommandResult comres = _bridge.GetListObjects<Scene>();

            if (comres.Success)
            {
                scenelist = (Dictionary<string, Scene>) comres.resultobject;
                foreach (KeyValuePair<string, Scene> kvp in scenelist)
                {
                    if (kvp.Value.name.StartsWith("HIDDEN"))
                    {
                        if (WinHueSettings.settings.ShowHiddenScenes)
                            scenelist.Add(kvp.Key, kvp.Value);
                    }
                    else
                    {
                        scenelist.Add(kvp.Key, kvp.Value);
                    }
                }

                cbObject.ItemsSource = scenelist;

                button = -1;
                _sensorid = sensorid;
            }
        }

        private void btnDone_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void btnOne_Click(object sender, RoutedEventArgs e)
        {
            tbBtnSelected.Text = string.Format(GlobalStrings.HueTap_ButtonSelected,1);
            button = 34;
           
            btnOne.Background = new SolidColorBrush(selectedColor);
            btnTwo.Background = new SolidColorBrush(deselectedColor);
            btnThree.Background = new SolidColorBrush(deselectedColor);
            btnFour.Background = new SolidColorBrush(deselectedColor);
        }

        private void btnTwo_Click(object sender, RoutedEventArgs e)
        {
            tbBtnSelected.Text = string.Format(GlobalStrings.HueTap_ButtonSelected, 2);
            button = 16;

            btnOne.Background = new SolidColorBrush(deselectedColor);
            btnTwo.Background = new SolidColorBrush(selectedColor);
            btnThree.Background = new SolidColorBrush(deselectedColor);
            btnFour.Background = new SolidColorBrush(deselectedColor);

        }

        private void btnFour_Click(object sender, RoutedEventArgs e)
        {
            tbBtnSelected.Text = string.Format(GlobalStrings.HueTap_ButtonSelected, 4);
            button = 18;

            btnOne.Background = new SolidColorBrush(deselectedColor);
            btnTwo.Background = new SolidColorBrush(deselectedColor);
            btnThree.Background = new SolidColorBrush(deselectedColor);
            btnFour.Background = new SolidColorBrush(selectedColor);

        }

        private void btnThree_Click(object sender, RoutedEventArgs e)
        {
            tbBtnSelected.Text = string.Format(GlobalStrings.HueTap_ButtonSelected, 3);
            button = 17;

            btnOne.Background = new SolidColorBrush(deselectedColor);
            btnTwo.Background = new SolidColorBrush(deselectedColor);
            btnThree.Background = new SolidColorBrush(selectedColor);
            btnFour.Background = new SolidColorBrush(deselectedColor);

        }

        private void cbObject_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnAssign_Click(object sender, RoutedEventArgs e)
        {
            if(button != -1)
            {
                if (cbObject.SelectedItem != null)
                {
                    Rule newRule = new Rule
                    {
                        name = string.Format("TAP {0}", button),
                        actions = new List<RuleAction>
                        {
                            new RuleAction()
                            {
                                address = "/groups/0/action",
                                body = new SceneBody() {scene = ((KeyValuePair<string,Scene>) cbObject.SelectedItem).Key},
                                method = "PUT"
                            }
                        },
                        conditions = new List<RuleCondition>
                        {
                            new RuleCondition()
                            {
                                address = string.Format("/sensors/{0}/state/buttonevent", _sensorid),
                                op = "eq",
                                value = button.ToString()
                            },
                            new RuleCondition()
                            {
                                address = string.Format("/sensors/{0}/state/lastupdated", _sensorid),
                                op = "dx"
                            }
                        }
                    };

                    CommandResult comres = _bridge.CreateObject<Rule>(newRule);
                    if (comres.Success)
                    {
                        btnOne.Background = new SolidColorBrush(deselectedColor);
                        btnTwo.Background = new SolidColorBrush(deselectedColor);
                        btnThree.Background = new SolidColorBrush(deselectedColor);
                        btnFour.Background = new SolidColorBrush(deselectedColor);
                        button = -1;
                        cbObject.SelectedItem = null;
                    }
                    else
                    {
                        _bridge.ShowErrorMessages();
                    }

                }
                else
                {
                    MessageBox.Show(GlobalStrings.HueTap_SelectAScene, GlobalStrings.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show(GlobalStrings.HueTap_SelectAButton, GlobalStrings.Error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
