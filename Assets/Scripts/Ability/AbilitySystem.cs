using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySystem : MonoBehaviour {

    public Bullet BulletPrefab;

    public List<string> Buttons = new List<string>();
    public AnimationCurve AbilityLearnCurve = new AnimationCurve();


    private void Update() {
        List<Ability> playerAbilities = Player.GetAbilities();
        int abilityCount = Mathf.FloorToInt(AbilityLearnCurve.Evaluate(Time.time));
        if(abilityCount >= playerAbilities.Count) {

            Ability a = null;
            switch (playerAbilities.Count) {
                case 0:
                    ShootAbility shootAbility = new ShootAbility();
                    shootAbility.BulletPrefab = BulletPrefab;
                    shootAbility.Damage = 1;
                    shootAbility.Button = "mouse 0";
                    shootAbility.BulletForce = 10f;
                    shootAbility.CoolDown = .2f;
                    a = shootAbility;
                    break;
                default:
                    a = CreateRandomAbility();                 
                    break;
            }
            //print("Added Ability, Time: " + Time.time);
            print(a);
            playerAbilities.Add(a);
        }
    }

    public Ability CreateRandomAbility() {
        Ability a = null;
        switch (Random.Range(0, 2)) {
            case 0:
                a = new ConeShotAbility();
                break;
            case 1:
                a = new RapidFireAbility();
                break;
            default:
                //a = new ConeShotAbility();
                //a = new RapidFireAbility();
                break;
        }

        int buttonIndex = Random.Range(0, Buttons.Count);
        a.Button = Buttons[buttonIndex];
        Buttons.RemoveAt(buttonIndex);

        a.BulletPrefab = BulletPrefab;
        a.Damage = 1;

        a.Init();
        return a;
    }

}
