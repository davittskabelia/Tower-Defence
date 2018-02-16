using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Spells
{
    class SpellButtonController : MonoBehaviour
    {
        public KeyCode KeyBind;
        public GameObject Border;
        public GameObject Recharge;
        public float RechargeTime;

        protected bool _active;
        protected float _cooldown;

        private static SpellButtonController _currentSpell;


        private SpellController _spell;

        public static SpellButtonController CurrentSpell
        {
            get { return _currentSpell; }
            protected set
            {
                if (_currentSpell != null) _currentSpell.Border.SetActive(false);
                _currentSpell = value;
                if (_currentSpell != null) _currentSpell.Border.SetActive(true);
            }
        }

        // Use this for initialization
        void Start()
        {
            _spell = GetComponent<SpellController>();

            _active = true;
            _cooldown = 0;
        }

        // Update is called once per frame
        void Update()
        {
            if (!_active)
            {
                _cooldown += Time.deltaTime;
                var x = Mathf.Clamp(_cooldown / RechargeTime, 0, 1);
                Recharge.transform.localScale = new Vector3(x, 1, 1);

                if (_cooldown >= RechargeTime) _active = true;
            }

            if (Input.GetKeyUp(KeyBind))
            {
                OnClick();
            }

            if (Input.GetMouseButtonDown(0) && !(EventSystem.current.IsPointerOverGameObject() &&
                                                 EventSystem.current.currentSelectedGameObject != null &&
                                                 EventSystem.current.currentSelectedGameObject
                                                     .GetComponent<CanvasRenderer>() != null))
            {
                if (SpellButtonController.CurrentSpell != null)
                {
                    SpellButtonController.CurrentSpell.OnCast();
                }
            }
        }

        public void OnClick()
        {
            CurrentSpell = this;
        }

        public void OnCast()
        {
            if (!_active) return;

            Recharge.transform.localScale = new Vector3(0, 1, 1);
            _cooldown = 0;
            _active = false;

            _spell.OnCast();
        }
    }
}