using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Skeleton;
using UnityEngine;

namespace Assets.Scripts.Spells
{
    class DirectActionSpell : SpellController
    {
        public override void OnCast()
        {
            RaycastHit hit;

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out hit)) return;

            CastAction(hit);

            InstantiateCastEffect(hit.point);
        }

        protected virtual void CastAction(RaycastHit hit)
        {
            var skeleton = hit.transform.gameObject.GetComponent<IDamagable>();
            if (skeleton != null) skeleton.Damage(Power);
        }
    }
}