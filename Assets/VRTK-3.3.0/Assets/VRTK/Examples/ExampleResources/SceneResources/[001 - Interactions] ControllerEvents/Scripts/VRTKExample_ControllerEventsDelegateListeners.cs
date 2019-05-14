namespace VRTK.Examples
{
    using UnityEngine;

    public class VRTKExample_ControllerEventsDelegateListeners : MonoBehaviour
    {
        public enum EventQuickSelect
        {
            Custom,
            None,
            All,
            ButtonOnly,
            AxisOnly,
            SenseAxisOnly
        }

        public VRTK_ControllerEvents controllerEvents;

        [Header("Quick Select")]

        public EventQuickSelect quickSelect = EventQuickSelect.All;

        [Header("Button Events Debug")]

        public bool triggerButtonEvents = true;
        public bool gripButtonEvents = true;
        public bool touchpadButtonEvents = true;
        public bool touchpadTwoButtonEvents = true;
        public bool buttonOneButtonEvents = true;
        public bool buttonTwoButtonEvents = true;
        public bool startMenuButtonEvents = true;

        [Header("Axis Events Debug")]

        public bool triggerAxisEvents = true;
        public bool gripAxisEvents = true;
        public bool touchpadAxisEvents = true;
        public bool touchpadTwoAxisEvents = true;

        [Header("Sense Axis Events Debug")]

        public bool triggerSenseAxisEvents = true;
        public bool touchpadSenseAxisEvents = true;
        public bool middleFingerSenseAxisEvents = true;
        public bool ringFingerSenseAxisEvents = true;
        public bool pinkyFingerSenseAxisEvents = true;

        private void OnEnable()
        {
            controllerEvents = (controllerEvents == null ? GetComponent<VRTK_ControllerEvents>() : controllerEvents);
            if (controllerEvents == null)
            {
                VRTK_Logger.Error(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_FROM_GAMEOBJECT, "VRTK_ControllerEvents_ListenerExample VRTK_ControllerEvents the same"));
                return;
            }

            //Setup controller event listeners
            controllerEvents.TriggerPressed += DoTriggerPressed;
            controllerEvents.TriggerReleased += DoTriggerReleased;
            controllerEvents.TriggerTouchStart += DoTriggerTouchStart;
            controllerEvents.TriggerTouchEnd += DoTriggerTouchEnd;
            controllerEvents.TriggerHairlineStart += DoTriggerHairlineStart;
            controllerEvents.TriggerHairlineEnd += DoTriggerHairlineEnd;
            controllerEvents.TriggerClicked += DoTriggerClicked;
            controllerEvents.TriggerUnclicked += DoTriggerUnclicked;
            controllerEvents.TriggerAxisChanged += DoTriggerAxisChanged;
            controllerEvents.TriggerSenseAxisChanged += DoTriggerSenseAxisChanged;

            controllerEvents.GripPressed += DoGripPressed;
            controllerEvents.GripReleased += DoGripReleased;
            controllerEvents.GripTouchStart += DoGripTouchStart;
            controllerEvents.GripTouchEnd += DoGripTouchEnd;
            controllerEvents.GripHairlineStart += DoGripHairlineStart;
            controllerEvents.GripHairlineEnd += DoGripHairlineEnd;
            controllerEvents.GripClicked += DoGripClicked;
            controllerEvents.GripUnclicked += DoGripUnclicked;
            controllerEvents.GripAxisChanged += DoGripAxisChanged;

            controllerEvents.TouchpadPressed += DoTouchpadPressed;
            controllerEvents.TouchpadReleased += DoTouchpadReleased;
            controllerEvents.TouchpadTouchStart += DoTouchpadTouchStart;
            controllerEvents.TouchpadTouchEnd += DoTouchpadTouchEnd;
            controllerEvents.TouchpadAxisChanged += DoTouchpadAxisChanged;
            controllerEvents.TouchpadTwoPressed += DoTouchpadTwoPressed;
            controllerEvents.TouchpadTwoReleased += DoTouchpadTwoReleased;
            controllerEvents.TouchpadTwoTouchStart += DoTouchpadTwoTouchStart;
            controllerEvents.TouchpadTwoTouchEnd += DoTouchpadTwoTouchEnd;
            controllerEvents.TouchpadTwoAxisChanged += DoTouchpadTwoAxisChanged;
            controllerEvents.TouchpadSenseAxisChanged += DoTouchpadSenseAxisChanged;

            controllerEvents.ButtonOnePressed += DoButtonOnePressed;
            controllerEvents.ButtonOneReleased += DoButtonOneReleased;
            controllerEvents.ButtonOneTouchStart += DoButtonOneTouchStart;
            controllerEvents.ButtonOneTouchEnd += DoButtonOneTouchEnd;

            controllerEvents.ButtonTwoPressed += DoButtonTwoPressed;
            controllerEvents.ButtonTwoReleased += DoButtonTwoReleased;
            controllerEvents.ButtonTwoTouchStart += DoButtonTwoTouchStart;
            controllerEvents.ButtonTwoTouchEnd += DoButtonTwoTouchEnd;

            controllerEvents.StartMenuPressed += DoStartMenuPressed;
            controllerEvents.StartMenuReleased += DoStartMenuReleased;

            controllerEvents.ControllerEnabled += DoControllerEnabled;
            controllerEvents.ControllerDisabled += DoControllerDisabled;
            controllerEvents.ControllerIndexChanged += DoControllerIndexChanged;

            controllerEvents.MiddleFingerSenseAxisChanged += DoMiddleFingerSenseAxisChanged;
            controllerEvents.RingFingerSenseAxisChanged += DoRingFingerSenseAxisChanged;
            controllerEvents.PinkyFingerSenseAxisChanged += DoPinkyFingerSenseAxisChanged;
        }

        private void OnDisable()
        {
            if (controllerEvents != null)
            {
                controllerEvents.TriggerPressed -= DoTriggerPressed;
                controllerEvents.TriggerReleased -= DoTriggerReleased;
                controllerEvents.TriggerTouchStart -= DoTriggerTouchStart;
                controllerEvents.TriggerTouchEnd -= DoTriggerTouchEnd;
                controllerEvents.TriggerHairlineStart -= DoTriggerHairlineStart;
                controllerEvents.TriggerHairlineEnd -= DoTriggerHairlineEnd;
                controllerEvents.TriggerClicked -= DoTriggerClicked;
                controllerEvents.TriggerUnclicked -= DoTriggerUnclicked;
                controllerEvents.TriggerAxisChanged -= DoTriggerAxisChanged;
                controllerEvents.TriggerSenseAxisChanged -= DoTriggerSenseAxisChanged;

                controllerEvents.GripPressed -= DoGripPressed;
                controllerEvents.GripReleased -= DoGripReleased;
                controllerEvents.GripTouchStart -= DoGripTouchStart;
                controllerEvents.GripTouchEnd -= DoGripTouchEnd;
                controllerEvents.GripHairlineStart -= DoGripHairlineStart;
                controllerEvents.GripHairlineEnd -= DoGripHairlineEnd;
                controllerEvents.GripClicked -= DoGripClicked;
                controllerEvents.GripUnclicked -= DoGripUnclicked;
                controllerEvents.GripAxisChanged -= DoGripAxisChanged;

                controllerEvents.TouchpadPressed -= DoTouchpadPressed;
                controllerEvents.TouchpadReleased -= DoTouchpadReleased;
                controllerEvents.TouchpadTouchStart -= DoTouchpadTouchStart;
                controllerEvents.TouchpadTouchEnd -= DoTouchpadTouchEnd;
                controllerEvents.TouchpadAxisChanged -= DoTouchpadAxisChanged;
                controllerEvents.TouchpadTwoPressed -= DoTouchpadTwoPressed;
                controllerEvents.TouchpadTwoReleased -= DoTouchpadTwoReleased;
                controllerEvents.TouchpadTwoTouchStart -= DoTouchpadTwoTouchStart;
                controllerEvents.TouchpadTwoTouchEnd -= DoTouchpadTwoTouchEnd;
                controllerEvents.TouchpadTwoAxisChanged -= DoTouchpadTwoAxisChanged;
                controllerEvents.TouchpadSenseAxisChanged -= DoTouchpadSenseAxisChanged;

                controllerEvents.ButtonOnePressed -= DoButtonOnePressed;
                controllerEvents.ButtonOneReleased -= DoButtonOneReleased;
                controllerEvents.ButtonOneTouchStart -= DoButtonOneTouchStart;
                controllerEvents.ButtonOneTouchEnd -= DoButtonOneTouchEnd;

                controllerEvents.ButtonTwoPressed -= DoButtonTwoPressed;
                controllerEvents.ButtonTwoReleased -= DoButtonTwoReleased;
                controllerEvents.ButtonTwoTouchStart -= DoButtonTwoTouchStart;
                controllerEvents.ButtonTwoTouchEnd -= DoButtonTwoTouchEnd;

                controllerEvents.StartMenuPressed -= DoStartMenuPressed;
                controllerEvents.StartMenuReleased -= DoStartMenuReleased;

                controllerEvents.ControllerEnabled -= DoControllerEnabled;
                controllerEvents.ControllerDisabled -= DoControllerDisabled;
                controllerEvents.ControllerIndexChanged -= DoControllerIndexChanged;

                controllerEvents.MiddleFingerSenseAxisChanged -= DoMiddleFingerSenseAxisChanged;
                controllerEvents.RingFingerSenseAxisChanged -= DoRingFingerSenseAxisChanged;
                controllerEvents.PinkyFingerSenseAxisChanged -= DoPinkyFingerSenseAxisChanged;
            }
        }

        private void LateUpdate()
        {
            switch (quickSelect)
            {
                case EventQuickSelect.None:
                    triggerButtonEvents = false;
                    gripButtonEvents = false;
                    touchpadButtonEvents = false;
                    touchpadTwoButtonEvents = false;
                    buttonOneButtonEvents = false;
                    buttonTwoButtonEvents = false;
                    startMenuButtonEvents = false;

                    triggerAxisEvents = false;
                    gripAxisEvents = false;
                    touchpadAxisEvents = false;
                    touchpadTwoAxisEvents = false;

                    triggerSenseAxisEvents = false;
                    touchpadSenseAxisEvents = false;
                    middleFingerSenseAxisEvents = false;
                    ringFingerSenseAxisEvents = false;
                    pinkyFingerSenseAxisEvents = false;
                    break;
                case EventQuickSelect.All:
                    triggerButtonEvents = true;
                    gripButtonEvents = true;
                    touchpadButtonEvents = true;
                    touchpadTwoButtonEvents = true;
                    buttonOneButtonEvents = true;
                    buttonTwoButtonEvents = true;
                    startMenuButtonEvents = true;

                    triggerAxisEvents = true;
                    gripAxisEvents = true;
                    touchpadAxisEvents = true;
                    touchpadTwoAxisEvents = true;

                    triggerSenseAxisEvents = true;
                    touchpadSenseAxisEvents = true;
                    middleFingerSenseAxisEvents = true;
                    ringFingerSenseAxisEvents = true;
                    pinkyFingerSenseAxisEvents = true;
                    break;
                case EventQuickSelect.ButtonOnly:
                    triggerButtonEvents = true;
                    gripButtonEvents = true;
                    touchpadButtonEvents = true;
                    touchpadTwoButtonEvents = true;
                    buttonOneButtonEvents = true;
                    buttonTwoButtonEvents = true;
                    startMenuButtonEvents = true;

                    triggerAxisEvents = false;
                    gripAxisEvents = false;
                    touchpadAxisEvents = false;
                    touchpadTwoAxisEvents = false;

                    triggerSenseAxisEvents = false;
                    touchpadSenseAxisEvents = false;
                    middleFingerSenseAxisEvents = false;
                    ringFingerSenseAxisEvents = false;
                    pinkyFingerSenseAxisEvents = false;
                    break;
                case EventQuickSelect.AxisOnly:
                    triggerButtonEvents = false;
                    gripButtonEvents = false;
                    touchpadButtonEvents = false;
                    touchpadTwoButtonEvents = false;
                    buttonOneButtonEvents = false;
                    buttonTwoButtonEvents = false;
                    startMenuButtonEvents = false;

                    triggerAxisEvents = true;
                    gripAxisEvents = true;
                    touchpadAxisEvents = true;
                    touchpadTwoAxisEvents = true;

                    triggerSenseAxisEvents = false;
                    touchpadSenseAxisEvents = false;
                    middleFingerSenseAxisEvents = false;
                    ringFingerSenseAxisEvents = false;
                    pinkyFingerSenseAxisEvents = false;
                    break;
                case EventQuickSelect.SenseAxisOnly:
                    triggerButtonEvents = false;
                    gripButtonEvents = false;
                    touchpadButtonEvents = false;
                    touchpadTwoButtonEvents = false;
                    buttonOneButtonEvents = false;
                    buttonTwoButtonEvents = false;
                    startMenuButtonEvents = false;

                    triggerAxisEvents = false;
                    gripAxisEvents = false;
                    touchpadAxisEvents = false;
                    touchpadTwoAxisEvents = false;

                    triggerSenseAxisEvents = true;
                    touchpadSenseAxisEvents = true;
                    middleFingerSenseAxisEvents = true;
                    ringFingerSenseAxisEvents = true;
                    pinkyFingerSenseAxisEvents = true;
                    break;
            }
        }

        private void DebugLogger(uint index, string button, string action, ControllerInteractionEventArgs e)
        {
            string debugString = "Controller on index '" + index + "' " + button + " has been " + action
                                 + " with a pressure of " + e.buttonPressure + " / Primary Touchpad axis at: " + e.touchpadAxis + " (" + e.touchpadAngle + " degrees)" + " / Secondary Touchpad axis at: " + e.touchpadTwoAxis + " (" + e.touchpadTwoAngle + " degrees)";
            VRTK_Logger.Info(debugString);
        }

        private void DoTriggerPressed(object sender, ControllerInteractionEventArgs e)
        {
            if (triggerButtonEvents)
            {
                Debug.Log("TRIGGER pressed");
            }
        }

        private void DoTriggerReleased(object sender, ControllerInteractionEventArgs e)
        {
            if (triggerButtonEvents)
            {
                Debug.Log("TRIGGER released");
            }
        }

        private void DoTriggerTouchStart(object sender, ControllerInteractionEventArgs e)
        {
            if (triggerButtonEvents)
            {
                Debug.Log("TRIGGER touched");
            }
        }

        private void DoTriggerTouchEnd(object sender, ControllerInteractionEventArgs e)
        {
            if (triggerButtonEvents)
            {
                Debug.Log("TRIGGER untouched");
            }
        }

        private void DoTriggerHairlineStart(object sender, ControllerInteractionEventArgs e)
        {
            if (triggerButtonEvents)
            {
                Debug.Log("TRIGGER hairline start");
            }
        }

        private void DoTriggerHairlineEnd(object sender, ControllerInteractionEventArgs e)
        {
            if (triggerButtonEvents)
            {
                Debug.Log("TRIGGER hairline end");
            }
        }

        private void DoTriggerClicked(object sender, ControllerInteractionEventArgs e)
        {
            if (triggerButtonEvents)
            {
                Debug.Log("TRIGGER clicked");
            }
        }

        private void DoTriggerUnclicked(object sender, ControllerInteractionEventArgs e)
        {
            if (triggerButtonEvents)
            {
                Debug.Log("TRIGGER unclicked");
            }
        }

        private void DoTriggerAxisChanged(object sender, ControllerInteractionEventArgs e)
        {
            if (triggerAxisEvents)
            {
                Debug.Log("TRIGGER axis changed");
            }
        }

        private void DoTriggerSenseAxisChanged(object sender, ControllerInteractionEventArgs e)
        {
            if (triggerSenseAxisEvents)
            {
                Debug.Log("TRIGGER sense axis changed");
            }
        }

        private void DoGripPressed(object sender, ControllerInteractionEventArgs e)
        {
            if (gripButtonEvents)
            {
                Debug.Log("GRIP pressed");
            }
        }

        private void DoGripReleased(object sender, ControllerInteractionEventArgs e)
        {
            if (gripButtonEvents)
            {
                Debug.Log("GRIP released");
            }
        }

        private void DoGripTouchStart(object sender, ControllerInteractionEventArgs e)
        {
            if (gripButtonEvents)
            {
                Debug.Log("GRIP touched");
            }
        }

        private void DoGripTouchEnd(object sender, ControllerInteractionEventArgs e)
        {
            if (gripButtonEvents)
            {
                Debug.Log("GRIP untouched");
            }
        }

        private void DoGripHairlineStart(object sender, ControllerInteractionEventArgs e)
        {
            if (gripButtonEvents)
            {
                Debug.Log("GRIP hairline start");
            }
        }

        private void DoGripHairlineEnd(object sender, ControllerInteractionEventArgs e)
        {
            if (gripButtonEvents)
            {
                Debug.Log("GRIP hairline end");
            }
        }

        private void DoGripClicked(object sender, ControllerInteractionEventArgs e)
        {
            if (gripButtonEvents)
            {
                Debug.Log("GRIP clicked");
            }
        }

        private void DoGripUnclicked(object sender, ControllerInteractionEventArgs e)
        {
            if (gripButtonEvents)
            {
                Debug.Log("GRIP unclicked");
            }
        }

        private void DoGripAxisChanged(object sender, ControllerInteractionEventArgs e)
        {
            if (gripAxisEvents)
            {
                Debug.Log("GRIP axis changed");
            }
        }

        private void DoTouchpadPressed(object sender, ControllerInteractionEventArgs e)
        {
            if (touchpadButtonEvents)
            {
                Debug.Log("TOUCHPAD pressed down");
            }
        }

        private void DoTouchpadReleased(object sender, ControllerInteractionEventArgs e)
        {
            if (touchpadButtonEvents)
            {
                Debug.Log("TOUCHPAD released");
            }
        }

        private void DoTouchpadTouchStart(object sender, ControllerInteractionEventArgs e)
        {
            if (touchpadButtonEvents)
            {
                Debug.Log("TOUCHPAD touched");
            }
        }

        private void DoTouchpadTouchEnd(object sender, ControllerInteractionEventArgs e)
        {
            if (touchpadButtonEvents)
            {
                Debug.Log("TOUCHPAD untouched");
            }
        }

        private void DoTouchpadAxisChanged(object sender, ControllerInteractionEventArgs e)
        {
            if (touchpadAxisEvents)
            {
                Debug.Log("TOUCHPAD axis changed");
            }
        }

        private void DoTouchpadTwoPressed(object sender, ControllerInteractionEventArgs e)
        {
            if (touchpadTwoButtonEvents)
            {
                Debug.Log("TOUCHPADTWO pressed down");
            }
        }

        private void DoTouchpadTwoReleased(object sender, ControllerInteractionEventArgs e)
        {
            if (touchpadTwoButtonEvents)
            {
                Debug.Log("TOUCHPADTWO released");
            }
        }

        private void DoTouchpadTwoTouchStart(object sender, ControllerInteractionEventArgs e)
        {
            if (touchpadTwoButtonEvents)
            {
                Debug.Log("TOUCHPADTWO touched");
            }
        }

        private void DoTouchpadTwoTouchEnd(object sender, ControllerInteractionEventArgs e)
        {
            if (touchpadTwoButtonEvents)
            {
                Debug.Log("TOUCHPADTWO untouched");
            }
        }

        private void DoTouchpadTwoAxisChanged(object sender, ControllerInteractionEventArgs e)
        {
            if (touchpadTwoAxisEvents)
            {
                Debug.Log("TOUCHPADTWO axis changed");
            }
        }

        private void DoTouchpadSenseAxisChanged(object sender, ControllerInteractionEventArgs e)
        {
            if (touchpadSenseAxisEvents)
            {
                Debug.Log("TOUCHPAD sense axis changed");
            }
        }

        private void DoButtonOnePressed(object sender, ControllerInteractionEventArgs e)
        {
            if (buttonOneButtonEvents)
            {
                Debug.Log("BUTTON ONE pressed down");
            }
        }

        private void DoButtonOneReleased(object sender, ControllerInteractionEventArgs e)
        {
            if (buttonOneButtonEvents)
            {
                Debug.Log("BUTTON ONE released");
            }
        }

        private void DoButtonOneTouchStart(object sender, ControllerInteractionEventArgs e)
        {
            if (buttonOneButtonEvents)
            {
                Debug.Log("BUTTON ONE touched");
            }
        }

        private void DoButtonOneTouchEnd(object sender, ControllerInteractionEventArgs e)
        {
            if (buttonOneButtonEvents)
            {
                Debug.Log("BUTTON ONE untouched");
            }
        }

        private void DoButtonTwoPressed(object sender, ControllerInteractionEventArgs e)
        {
            if (buttonTwoButtonEvents)
            {
                Debug.Log("BUTTON TWO pressed down");
            }
        }

        private void DoButtonTwoReleased(object sender, ControllerInteractionEventArgs e)
        {
            if (buttonTwoButtonEvents)
            {
                Debug.Log("BUTTON TWO released");
            }
        }

        private void DoButtonTwoTouchStart(object sender, ControllerInteractionEventArgs e)
        {
            if (buttonTwoButtonEvents)
            {
                Debug.Log("BUTTON TWO touched");
            }
        }

        private void DoButtonTwoTouchEnd(object sender, ControllerInteractionEventArgs e)
        {
            if (buttonTwoButtonEvents)
            {
                Debug.Log("BUTTON TWO untouched");
            }
        }

        private void DoStartMenuPressed(object sender, ControllerInteractionEventArgs e)
        {
            if (startMenuButtonEvents)
            {
                Debug.Log("START MENU pressed down");
            }
        }

        private void DoStartMenuReleased(object sender, ControllerInteractionEventArgs e)
        {
            if (startMenuButtonEvents)
            {
                Debug.Log("START MENU released");
            }
        }

        private void DoControllerEnabled(object sender, ControllerInteractionEventArgs e)
        {
            Debug.Log("CONTROLLER STATE ENABLED");
        }

        private void DoControllerDisabled(object sender, ControllerInteractionEventArgs e)
        {
            Debug.Log("CONTROLLER STATE DISABLED");
        }

        private void DoControllerIndexChanged(object sender, ControllerInteractionEventArgs e)
        {
            Debug.Log("CONTROLLER STATE INDEX CHANGED");
        }

        private void DoMiddleFingerSenseAxisChanged(object sender, ControllerInteractionEventArgs e)
        {
            if (middleFingerSenseAxisEvents)
            {
                Debug.Log("MIDDLE FINGER sense axis changed");
            }
        }

        private void DoRingFingerSenseAxisChanged(object sender, ControllerInteractionEventArgs e)
        {
            if (ringFingerSenseAxisEvents)
            {
                Debug.Log("RING FINGER sense axis changed");
            }
        }

        private void DoPinkyFingerSenseAxisChanged(object sender, ControllerInteractionEventArgs e)
        {
            if (pinkyFingerSenseAxisEvents)
            {
                Debug.Log("PINKY FINGER sense axis changed");
            }
        }
    }
}