using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Skeleton;
using UnityEngine;

namespace Assets.Scripts.Spells
{
    class RadiusFreezeSpell : SphereActionSpell
    {
        protected override void CastAction(RaycastHit[] hits)
        {
            foreach (var damagable in hits.Select(hit => hit.transform.gameObject.GetComponent<IFreezable>())
                .Where(d => d != null))
            {
                damagable.Freeze(Power);
            }
        }
    }
}