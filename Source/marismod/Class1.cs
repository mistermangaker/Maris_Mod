using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using UnityEngine;
using Verse;
using Verse.AI;
using Verse.AI.Group;

namespace Maris_mod
{
    [DefOf]
    public static class Neko_Defs
    {
        public static GeneDef Neko_Distractable;
       
    }
    [DefOf]
    

    public class MentalState_zoomies : MentalState
    {
        public override RandomSocialMode SocialModeMax()
        {
            return RandomSocialMode.SuperActive;
        }
    }


    public class JobGiver_PanicFreezing : ThinkNode_JobGiver
    {
        protected override Job TryGiveJob(Pawn pawn)
        {
            List<Gene> genesListForReading = pawn.genes.GenesListForReading;
            for (int i = 0; i < genesListForReading.Count; i++)
            {
                if (genesListForReading[i].Active && genesListForReading[i].def == Neko_Defs.Neko_Distractable)
                {
                    //Log.Message("correct genes!");
                    Job job;
                    List<JoyGiverDef> allDefsListForReading = DefDatabase<JoyGiverDef>.AllDefsListForReading;
                    IEnumerable<JoyGiverDef> joyGiverDefs =
                        from j in allDefsListForReading
                        where j.Worker.MissingRequiredCapacity(pawn) == null
                        select j;
                    if (joyGiverDefs.Count<JoyGiverDef>() != 0)
                    {
                        JoyGiverDef joyGiverDef = GenCollection.RandomElement<JoyGiverDef>(joyGiverDefs);
                        job = joyGiverDef.Worker.TryGiveJob(pawn);
                        job.locomotionUrgency = LocomotionUrgency.Sprint;
                        return job;
                    }
                    else
                    {

                        return null;
                    }


                }

            }
            return null;

        }
    }



    public class MentalState_DoSomethingElse : MentalState
    {
        public MentalState_DoSomethingElse() { }
        public override void MentalStateTick()
        {
            Job job;
            List<JoyGiverDef> allDefsListForReading = DefDatabase<JoyGiverDef>.AllDefsListForReading;
            IEnumerable<JoyGiverDef> joyGiverDefs =
                from j in allDefsListForReading
                where j.Worker.MissingRequiredCapacity(pawn) == null
                select j;
            if (joyGiverDefs.Count<JoyGiverDef>() != 0)
            {
                JoyGiverDef joyGiverDef = GenCollection.RandomElement<JoyGiverDef>(joyGiverDefs);
                job = joyGiverDef.Worker.TryGiveJob(pawn);
            }
            else
            {
               
                job = null;
            }
            
            base.MentalStateTick();
        }
        
        
    }












}