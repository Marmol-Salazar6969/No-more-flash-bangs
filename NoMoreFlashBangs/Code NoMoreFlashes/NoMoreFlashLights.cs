using BepInEx;
using System;
using UnityEngine;
using System.Reflection;
using MonoMod.RuntimeDetour;
using On;
using IL;

namespace NoMoreFlashes;
[BepInPlugin("BensoneWhite.NoMoreFlashes", "NoMoreFlashes", "1.2")]
public class MfPXMod : BaseUnityPlugin
{
    public float LightIntensity { get; private set; }
    public bool SlatedForDeletetion { get; private set; }

    public LightSource lightsource;

    public RainWorldGame game;

    private void LogInfo(object data)
    {
        Logger.LogInfo(data);
    }

    public void OnEnable()
    {
        LogInfo("MY EYES is now Enabled");
        On.RainWorld.OnModsInit += RainWorld_OnModsInit;
    }
    private void RainWorld_OnModsInit(On.RainWorld.orig_OnModsInit orig, RainWorld self)
    {
        try
        {
            On.FlareBomb.DrawSprites += FlareBomb_DrawSprites;
            On.FlareBomb.Update += FlareBomb_Update;
            On.FlareBomb.HitSomething += FlareBomb_HitSomething;
            On.GreenSparks.GreenSpark.DrawSprites += GreenSpark_DrawSprites;
            On.ZapCoil.ZapFlash.DrawSprites += ZapFlash_DrawSprites;
            On.ZapCoil.ZapFlash.InitiateSprites += ZapFlash_InitiateSprites;
            On.RainCycle.Update += RainCycle_Update;
            On.ZapCoil.DrawSprites += ZapCoil_DrawSprites;
            On.ZapCoil.InitiateSprites += ZapCoil_InitiateSprites;
            On.ZapCoil.AddToContainer += ZapCoil_AddToContainer;
            On.UnderwaterShock.Flash.InitiateSprites += Flash_InitiateSprites;
            On.ElectricDeath.DrawSprites += ElectricDeath_DrawSprites;
            On.ElectricDeath.SparkFlash.DrawSprites += SparkFlash_DrawSprites;
            On.ElectricDeath.LightFlash.Draw += LightFlash_Draw;
            On.ElectricDeath.InitiateSprites += ElectricDeath_InitiateSprites;
            On.ElectricDeath.SparkFlash.InitiateSprites += SparkFlash_InitiateSprites;
        }
        catch (Exception data)
        {
            LogInfo(data);
            throw;
        }
        finally
        {
            orig.Invoke(self);
        }
    }

    private void SparkFlash_InitiateSprites(On.ElectricDeath.SparkFlash.orig_InitiateSprites orig, ElectricDeath.SparkFlash self, RoomCamera.SpriteLeaser sLeaser, RoomCamera rCam)
    {
        orig(self, sLeaser, rCam);
        sLeaser.sprites[0].isVisible = false;
        sLeaser.sprites[1].isVisible = false;
        sLeaser.sprites[2].isVisible = false;
    }
    private void ElectricDeath_InitiateSprites(On.ElectricDeath.orig_InitiateSprites orig, ElectricDeath self, RoomCamera.SpriteLeaser sLeaser, RoomCamera rCam)
    {
        orig(self, sLeaser, rCam);
        sLeaser.sprites[0].isVisible = true;
        sLeaser.sprites[1].isVisible = false;
        sLeaser.sprites[2].isVisible = false;
    }
    private void LightFlash_Draw(On.ElectricDeath.LightFlash.orig_Draw orig, ElectricDeath.LightFlash self, RoomCamera.SpriteLeaser sLeaser, RoomCamera rCam, float timeStacker, Vector2 camPos)
    {
        orig(self, sLeaser, rCam, timeStacker, camPos);
        sLeaser.sprites[0].isVisible = false;
    }
    private void SparkFlash_DrawSprites(On.ElectricDeath.SparkFlash.orig_DrawSprites orig, ElectricDeath.SparkFlash self, RoomCamera.SpriteLeaser sLeaser, RoomCamera rCam, float timeStacker, Vector2 camPos)
    {
        orig(self, sLeaser, rCam, timeStacker, camPos);
        sLeaser.sprites[0].isVisible = false; 
    }
    private void ElectricDeath_DrawSprites(On.ElectricDeath.orig_DrawSprites orig, ElectricDeath self, RoomCamera.SpriteLeaser sLeaser, RoomCamera rCam, float timeStacker, Vector2 camPos)
    {
        orig(self, sLeaser, rCam, timeStacker, camPos);
        sLeaser.sprites[0].isVisible = false;
        sLeaser.sprites[1].isVisible = false;
    }
    private void Flash_InitiateSprites(On.UnderwaterShock.Flash.orig_InitiateSprites orig, UnderwaterShock.Flash self, RoomCamera.SpriteLeaser sLeaser, RoomCamera rCam)
    {
        orig(self, sLeaser, rCam);
        sLeaser.sprites[0].isVisible = false;
    }
    private void ZapCoil_AddToContainer(On.ZapCoil.orig_AddToContainer orig, ZapCoil self, RoomCamera.SpriteLeaser sLeaser, RoomCamera rCam, FContainer newContatiner)
    {
        orig(self, sLeaser, rCam, newContatiner);
        sLeaser.sprites[0].alpha = 0f;
    }
    private void ZapCoil_InitiateSprites(On.ZapCoil.orig_InitiateSprites orig, ZapCoil self, RoomCamera.SpriteLeaser sLeaser, RoomCamera rCam)
    {
        orig(self, sLeaser, rCam);
        sLeaser.sprites[0].isVisible = false;
    }
    private void ZapCoil_DrawSprites(On.ZapCoil.orig_DrawSprites orig, ZapCoil self, RoomCamera.SpriteLeaser sLeaser, RoomCamera rCam, float timeStacker, Vector2 camPos)
    {
        orig(self, sLeaser, rCam, timeStacker, camPos);
        sLeaser.sprites[0].isVisible = true;
    }
    private void RainCycle_Update(On.RainCycle.orig_Update orig, RainCycle self)
    {
        orig(self);
        self.world.game.globalRain.ScreenShake = 0.15f;
        self.world.game.globalRain.MicroScreenShake = 0.15f;
    }
    private void ZapFlash_InitiateSprites(On.ZapCoil.ZapFlash.orig_InitiateSprites orig, ZapCoil.ZapFlash self, RoomCamera.SpriteLeaser sLeaser, RoomCamera rCam)
    {
        orig(self, sLeaser, rCam);
        sLeaser.sprites[0].isVisible = false;
        sLeaser.sprites[1].isVisible = false;
    }
    private void ZapFlash_DrawSprites(On.ZapCoil.ZapFlash.orig_DrawSprites orig, ZapCoil.ZapFlash self, RoomCamera.SpriteLeaser sLeaser, RoomCamera rCam, float timeStacker, Vector2 camPos)
    {
        orig(self, sLeaser, rCam, timeStacker, camPos);
        sLeaser.sprites[0].isVisible = false;
        sLeaser.sprites[1].isVisible = false;
    }
    private void GreenSpark_DrawSprites(On.GreenSparks.GreenSpark.orig_DrawSprites orig, GreenSparks.GreenSpark self, RoomCamera.SpriteLeaser sLeaser, RoomCamera rCam, float timeStacker, Vector2 camPos)
    {
        orig(self, sLeaser, rCam, timeStacker, camPos);
        sLeaser.sprites[0].isVisible = false;
    }
    private bool FlareBomb_HitSomething(On.FlareBomb.orig_HitSomething orig, FlareBomb self, SharedPhysics.CollisionResult result, bool eu)
    {
        var room = self.room;
        orig(self, result, eu);
        room.PlaySound(SoundID.Rock_Hit_Creature, self.firstChunk);
        if (result.chunk == null)
        {
            room.PlaySound(SoundID.Slugcat_Throw_Flare_Bomb, self.firstChunk);
            room.PlaySound(SoundID.Flare_Bomb_Burn, self.firstChunk);
            room.PlaySound(SoundID.Centipede_Shock, self.firstChunk);
            self.burning = 0.899f;
            return false;
        }
        else
        {
            room.PlaySound(SoundID.Slugcat_Throw_Flare_Bomb, self.firstChunk);
            room.PlaySound(SoundID.Flare_Bomb_Burn, self.firstChunk);
            room.PlaySound(SoundID.Centipede_Shock, self.firstChunk);
            self.burning = 1.3f;
            self.Destroy();
            room.PlaySound(SoundID.Firecracker_Bang, self.firstChunk);
            return self.HitSomething(result, eu);

        }

    }
    private void FlareBomb_Update(On.FlareBomb.orig_Update orig, FlareBomb self, bool eu)
    {
        orig(self, eu);
        self.flashAplha = 0f;
        SlatedForDeletetion = true;
    }
    private void FlareBomb_DrawSprites(On.FlareBomb.orig_DrawSprites orig, FlareBomb self, RoomCamera.SpriteLeaser sLeaser, RoomCamera rCam, float timeStacker, Vector2 camPos)
    {
        orig(self, sLeaser, rCam, timeStacker, camPos);
        self.flashAplha = 0f;
        LightIntensity = 0f;
        SlatedForDeletetion = true;

    }
}