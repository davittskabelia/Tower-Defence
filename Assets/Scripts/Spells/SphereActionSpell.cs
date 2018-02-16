using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Skeleton;
using UnityEngine;

namespace Assets.Scripts.Spells
{
    class SphereActionSpell : SpellController
    {
        public float Radius;

        public override void OnCast()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var hits = Physics.RaycastAll(ray);

            if (hits.All(r => r.transform.tag != "Terrain")) return;

            var terrain = hits.First(r => r.transform.tag == "Terrain");

            hits = Physics.SphereCastAll(terrain.point, Radius, Vector3.up);

            CastAction(hits);

            InstantiateCastEffect(terrain.point);
        }

        protected virtual void CastAction(RaycastHit[] hits)
        {
            foreach (var damagable in hits.Select(hit => hit.transform.gameObject.GetComponent<IDamagable>())
                .Where(d => d != null))
            {
                damagable.Damage(Power);
            }
        }
    }
}