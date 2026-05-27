#nullable disable
using Il2CppTLD.Stats;

using System.Reflection;
using HarmonyLib;
using Il2Cpp;
using MelonLoader;
using ModSettings;
using UnityEngine;
using Il2CppTLD.Gear;

using System;
using System.Reflection;
using MelonLoader;
using HarmonyLib;
using Il2Cpp;
using Il2CppTLD.IntBackedUnit;
using ModSettings;
using UnityEngine;

[assembly: MelonInfo(typeof(testMod.testModMain), "AllInOneMod", "1.1.3", "626061157")]
[assembly: MelonGame("Hinterland", "TheLongDark")]

namespace testMod
{
    // ===================== Settings =====================
    internal class testModSettings : JsonModSettings
    {
        // ================= 信息显示 =================
        [Section("信息显示")]
        [Name("开启信息显示")]
        public bool showHUD = false;

        [Name("字体大小")]
        [Slider(15f, 30f)]
        public int fontSize = 20;

        [Name("显示天气")]
        public bool showWeather = true;

        [Name("天气X")]
        [Slider(0f, 2000f)]
        public float weatherX = 20f;

        [Name("天气Y")]
        [Slider(0f, 1200f)]
        public float weatherY = 20f;

        [Name("玩家信息")]
        public bool showPlayerInfo = true;

        [Name("玩家X")]
        [Slider(0f, 2000f)]
        public float playerX = 20f;

        [Name("玩家Y")]
        [Slider(0f, 1200f)]
        public float playerY = 60f;

        [Name("猎杀信息")]
        public bool showKillInfo = true;

        [Name("猎杀X")]
        [Slider(0f, 2000f)]
        public float killX = 20f;

        [Name("猎杀Y")]
        [Slider(0f, 1200f)]
        public float killY = 100f;

        // ================= 时间 =================
        [Section("时间控制")]
        [Name("全局变速并显示")]
        public bool enableTimeScale = false;

        [Name("时间倍率")]
        [Slider(0.1f, 10f, 100, NumberFormat = "{0:F1}")]
        public float timeScale = 1f;

        [Name("时间热键")]
        public KeyCode timeKey = KeyCode.Keypad8;

        // ================= 飞行 =================
        [Section("飞行模式")]
        [Name("一键飞行")]
        public bool enableFly = false;

        [Name("飞行热键")]
        public KeyCode flyKey = KeyCode.Keypad7;

        // ================= 战斗 =================
        [Section("战斗")]
   
        [Name("始终显示准星")]
        public bool alwaysShowCrosshair = false;

        [Name("伤害倍率")]
        [Slider(0.1f, 10f, 100, NumberFormat = "{0:F1}")]
        public float damageScale = 1f;

        [Name("击杀播报")]
        public bool killFeed = false;

        // ================= 生存 =================
        [Section("生存")]
        [Name("不扭伤")]
        public bool noSprain = false;

        [Name("薄冰无效")]
        public bool noThinIce = false;

        [Name("无限负重")]
        public bool enableExtraCarryCapacity = false;

        // [Name("毒气不窒息")]
        // public bool noGas = false;

        // // ================= 容器 =================
        [Section("容器")]
        [Name("修改容器容量")]
        public bool enableContainerCapacity = false;

        [Name("容器容量(kg)")]
        [Slider(10f, 1000f)]
        public float containerCapacity = 100f;

        // // ================= 采集 =================
        [Section("采集")]
        [Name("不产生碎肉")]
        public bool noRuinedMeat = false;

        [Name("肉皮肠收获时间倍率")]
        [Description("调整收获肉/皮/肠所需时间")]
        [Slider(0.1f, 1f, 10)]
        public float harvestTimeMultiplier = 1f;

        // [Name("跳过采集动画")]
        // public bool skipHarvestAnim = false;

        // [Name("快速采集")]
        // public bool fastHarvest = false;

        // ================= 生火 =================
        [Section("生火")]
        [Name("火堆无视风")]
        public bool fireIgnoreWind = false;

        [Name("放大镜无需阳光")]
        public bool lensNoSun = false;

        // [Name("燃料倍率")]
        // [Slider(0.1f, 10f)]
        // public float fuelMultiplier = 1f;

        [Name("火把不被风吹灭")]
        [Description("开启后火把不会因大风而熄灭（包括手持和丢出的火把）")]
        public bool torchNoWindExtinguish = false;
        [Name("增强火把加温")]
        public bool enableTorchHeat = false;

        [Name("火把额外温度")]
        [Description("在原有温度基础上增加的度数")]
        [Slider(0, 50)]
        public int torchExtraHeat = 0;

        [Name("手电筒无限电量")]
        [Description("开启后手电筒不会耗电，任何情况下均可照明")]
        public bool infiniteFlashlight = false;



        [Section("钓鱼")]
        [Name("钓鱼等待时间倍率")]
        [Description("0.1 = 更快上钩，2.0 = 更慢上钩")]
        [Slider(0.1f, 2f, 20)]
        public float fishingTimeMultiplier = 1f;

        // // ================= 烹饪 =================
        [Section("烹饪")]
        [Name("不烧焦食物")]
        public bool noBurnFood = false;

        // [Name("范围烹饪")]
        // public bool aoeCook = false;

        // ================= 折叠逻辑 =================
        protected override void OnChange(FieldInfo field, object oldValue, object newValue)
        {
            base.OnChange(field, oldValue, newValue);

            // HUD
            RefreshAll();
        }
        public void RefreshAll()
        {
            SetFieldVisible(nameof(fontSize), showHUD);

                SetFieldVisible(nameof(showWeather), showHUD);
                SetFieldVisible(nameof(weatherX), showHUD && showWeather);
                SetFieldVisible(nameof(weatherY), showHUD && showWeather);

                SetFieldVisible(nameof(showPlayerInfo), showHUD);
                SetFieldVisible(nameof(playerX), showHUD && showPlayerInfo);
                SetFieldVisible(nameof(playerY), showHUD && showPlayerInfo);

                SetFieldVisible(nameof(showKillInfo), showHUD);
                SetFieldVisible(nameof(killX), showHUD && showKillInfo);
                SetFieldVisible(nameof(killY), showHUD && showKillInfo);

                // 时间
                SetFieldVisible(nameof(timeScale), enableTimeScale);
                SetFieldVisible(nameof(timeKey), enableTimeScale);

                // 飞行
                SetFieldVisible(nameof(flyKey), enableFly);
                // 容器
                SetFieldVisible(nameof(containerCapacity), enableContainerCapacity);
                // 火把加温
                SetFieldVisible(nameof(torchExtraHeat), enableTorchHeat);
        }
    }


    // ===================== Settings管理 =====================
    internal static class Settings
    {
        public static testModSettings options;

        public static void OnLoad()
        {
            options = new testModSettings();
            options.AddToModSettings("综合修改");
            options.RefreshAll();
        }
    }

    // ===================== 主入口 =====================
    public class testModMain : MelonMod
    {
        private GUIStyle textStyle;
        
        private bool dragging1, dragging2, dragging3;
        private Vector2 dragOffset;

        internal static float currentSpeedMS;
        private bool timeActive = false;
        private WeatherTransition cachedWeatherTransition;

private bool lastLKeyState = false;


        public override void OnInitializeMelon()
        {
            Settings.OnLoad();
        }

        public override void OnUpdate()
        {
            UpdateFlyMode();
            UpdateTimeScale();
            // CheckCookingInfo(); 
            // UpdateCombat();
            // UpdateContainer();
        }

        public override void OnGUI()
        {
            if (!Settings.options.showHUD) return;
            if (GUI.skin == null) return;

            if (textStyle == null)
                textStyle = new GUIStyle(GUI.skin.label);

            textStyle.fontSize = Settings.options.fontSize;
            textStyle.normal.textColor = Color.white;

            DrawWeatherHUD();
            DrawPlayerHUD();
            DrawKillHUD();
            DrawTimeScaleHUD();
        }

        // 添加新的方法
private void CheckCookingInfo()
{
    // 只有在 noBurnFood 启用时才检测
    // if (!Settings.options.noBurnFood) return;
    
    bool currentLState = Input.GetKey(KeyCode.L);
    
    // 检测 L 键按下（边缘触发，避免重复打印）
    if (currentLState && !lastLKeyState)
    {
        PrintCurrentCookingInfo();
    }
    
    lastLKeyState = currentLState;
}

private void PrintCurrentCookingInfo()
{
    MelonLogger.Msg("========== 烹饪信息 ==========");
    
    // 方法1：查找所有 CookingPotItem
    var cookingPots = UnityEngine.Object.FindObjectsOfType<CookingPotItem>();
    
    if (cookingPots == null || cookingPots.Length == 0)
    {
        MelonLogger.Msg("未找到任何烹饪锅！");
        return;
    }
    
    int potIndex = 0;
    foreach (var pot in cookingPots)
    {
        MelonLogger.Msg($"--- 锅具 #{potIndex} ---");
        
        // 基本信息
        MelonLogger.Msg($"烹饪状态: {pot.m_CookingState}");
        MelonLogger.Msg($"状态枚举值: {(int)pot.m_CookingState}");
        
        // 正在烹饪的食物
        if (pot.m_GearItemBeingCooked != null)
        {
            string foodName = pot.m_GearItemBeingCooked.name;
            float percentCooked = pot.m_PercentCooked;
            float percentRuined = pot.m_PercentRuined;
            float minutesUntilCooked = pot.m_MinutesUntilCooked;
            float minutesUntilRuined = pot.m_MinutesUntilRuined;
            
            
            MelonLogger.Msg($"正在烹饪: {foodName}");
            MelonLogger.Msg($"烹饪进度: {percentCooked:P1}");  // 百分比格式
            MelonLogger.Msg($"烧焦进度: {percentRuined:P1}");
            MelonLogger.Msg($"距离煮熟: {minutesUntilCooked:F1} 分钟");
            MelonLogger.Msg($"距离烧焦: {minutesUntilRuined:F1} 分钟");
            
            // 尝试获取食物物品的详细信息
            var foodItem = pot.m_GearItemBeingCooked.GetComponent<FoodItem>();
            if (foodItem != null)
            {
                MelonLogger.Msg($"卡路里: {foodItem.m_CaloriesRemaining} cal");
                
            }
        }
        else
        {
            MelonLogger.Msg("没有正在烹饪的食物");

{
    MelonLogger.Msg("---- 水信息 ----");

    // 正在融化的雪
    MelonLogger.Msg(
        $"融雪量: {pot.m_LitersSnowBeingMelted.ToStringMetric(2)}"
    );

    // 正在煮的水
    MelonLogger.Msg(
        $"烧水量: {pot.m_LitersWaterBeingBoiled.ToStringMetric(2)}"
    );

    // 烹饪状态
    MelonLogger.Msg($"CookingState: {pot.m_CookingState}");

    // 煮熟进度
    MelonLogger.Msg($"PercentCooked: {pot.m_PercentCooked:P1}");

    // 烧焦进度
    MelonLogger.Msg($"PercentRuined: {pot.m_PercentRuined:P1}");

    // 距离完成
    MelonLogger.Msg(
        $"MinutesUntilCooked: {pot.m_MinutesUntilCooked:F1}"
    );

    // 距离烧干/烧焦
    MelonLogger.Msg(
        $"MinutesUntilRuined: {pot.m_MinutesUntilRuined:F1}"
    );
}
        }
        
        // 烧水信息
        
        
        // 附加信息
        var fire = pot.m_FireBeingUsed;
        if (fire != null)
        {
            MelonLogger.Msg($"火源存在: 是");
        }
        
        potIndex++;
    }
    
    MelonLogger.Msg("==============================");
}

        // ===================== 功能实现 =====================
        //信息显示
        private void DrawTimeScaleHUD()
        {
            if (!Settings.options.enableTimeScale) return;
            if (!timeActive) return;

            var oldColor = textStyle.normal.textColor;

            textStyle.normal.textColor = Color.green;

            string text = $"时间倍速 x{Settings.options.timeScale:F1}";

            float height = textStyle.CalcHeight(new GUIContent(text), 300f);

            GUI.Label(
                new Rect(20f, 20f, 300f, height),
                text,
                textStyle
            );

            textStyle.normal.textColor = oldColor;
        }
        private void DrawWeatherHUD()
        {
            if (!Settings.options.showWeather) return;

            string weatherText = GetWeatherText();


            Vector2 textSize = textStyle.CalcSize(new GUIContent(weatherText));
            float padding = 5f;          
            float extraWidth = 20f;       
            Rect rect = new Rect(
                Settings.options.weatherX,
                Settings.options.weatherY,
                textSize.x + padding * 2 ,
                textSize.y + padding 
            );
            Color bgColor = new Color(0f, 0f, 0f, 0.65f);   
            GUI.color = bgColor;
            GUI.Box(rect, GUIContent.none);
            GUI.color = Color.white;
            GUI.Label(new Rect(
                Settings.options.weatherX + padding,
                Settings.options.weatherY + padding,
                textSize.x + extraWidth,
                textSize.y
            ), weatherText, textStyle);
            HandleDrag(rect, ref dragging1, ref Settings.options.weatherX, ref Settings.options.weatherY);
        }
private string GetWeatherText()
{
    int days = Mathf.FloorToInt(StatsManager.GetValue(StatID.HoursSurvived) / 24f);
    var tod = TimeOfDay.Instance;
    string time = tod != null ? $"{tod.GetHour():D2}:{tod.GetMinutes():D2}" : "--:--";
    var weather = GameManager.GetWeatherComponent();
    string weatherStr = GetWeatherCN(weather);
    var wind = GameManager.GetWindComponent();
    string windStr = wind != null ? GetWindCN(wind.GetStrength()) : "--";
    float temp = weather != null ? weather.GetCurrentTemperature() : 0f;
    
    // 添加倒计时
    string countdown = "";
    var wt = GetWeatherTransition();
    if (wt != null && weather != null)
    {
        string debug = wt.GetDebugString();
        
        // 只匹配 >> 所在行的第一个 数字/数字hrs
        var lines = debug.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        foreach (var line in lines)
        {
            if (line.Contains(" >> "))
            {
                var match = System.Text.RegularExpressions.Regex.Match(line, @"(\d+\.?\d*)/(\d+\.?\d*)\s*hrs");
                if (match.Success)
                {
                    float elapsed = float.Parse(match.Groups[1].Value);
                    float total = float.Parse(match.Groups[2].Value);
                    float remaining = total - elapsed;
                    
                    if (remaining > 0.01f)
                    {
                        int hours = (int)remaining;
                        int minutes = (int)((remaining - hours) * 60);
                        countdown = $" 天气剩余:{hours}:{minutes:D2}";
                    }
                    else
                    {
                        countdown = " 天气即将变化";
                    }
                }
                break;
            }
        }
    }
    
    return $"生存 {days} 天  {time}  {weatherStr} {windStr} {temp:F0}°C{countdown}";
}
        private WeatherTransition GetWeatherTransition()
        {
            if (cachedWeatherTransition == null)
            {
                cachedWeatherTransition = UnityEngine.Object.FindObjectOfType<WeatherTransition>();
            }
            return cachedWeatherTransition;
        }
        private void DrawPlayerHUD()
        {
            if (!Settings.options.showPlayerInfo) return;

            string playerText = $"移速: {currentSpeedMS:F1} m/s  维C: {GetVitamin():F2}%";
            Vector2 textSize = textStyle.CalcSize(new GUIContent(playerText));
            float padding = 5f;
            float extraWidth = 20f;
            Rect rect = new Rect(
                Settings.options.playerX,
                Settings.options.playerY,
                textSize.x + padding * 2,
                textSize.y + padding
            );
            Color bgColor = new Color(0f, 0f, 0f, 0.65f);
            GUI.color = bgColor;
            GUI.Box(rect, GUIContent.none);
            GUI.color = Color.white;
            GUI.Label(new Rect(
                Settings.options.playerX + padding,
                Settings.options.playerY + padding,
                textSize.x + extraWidth,
                textSize.y
            ), playerText, textStyle);
            HandleDrag(rect, ref dragging2, ref Settings.options.playerX, ref Settings.options.playerY);
        }

        private float GetVitamin()
        {
            var scurvy = GameManager.GetScurvyComponent();
            return scurvy != null ? scurvy.GetVitaminCNormalized() * 100f : 0f;
        }

        private void DrawKillHUD()
        {
            if (!Settings.options.showKillInfo) return;

            string killText = GetKills();
            
            Vector2 textSize = textStyle.CalcSize(new GUIContent(killText));
            float padding = 5f;
            float extraWidth = 20f;
            
            Rect rect = new Rect(
                Settings.options.killX,
                Settings.options.killY,
                textSize.x + padding * 2,
                textSize.y + padding
            );
            
            Color bgColor = new Color(0f, 0f, 0f, 0.65f);
            GUI.color = bgColor;
            GUI.Box(rect, GUIContent.none);
            
            GUI.color = Color.white;
            GUI.Label(new Rect(
                Settings.options.killX + padding,
                Settings.options.killY + padding,
                textSize.x + extraWidth,
                textSize.y
            ), killText, textStyle);
            
            HandleDrag(rect, ref dragging3, ref Settings.options.killX, ref Settings.options.killY);
        }
        private string GetKills()
        {
            int rabbit = Mathf.FloorToInt(StatsManager.GetValue(StatID.RabbitsKilled));
            int ptarmigan = Mathf.FloorToInt(StatsManager.GetValue(StatID.PtarmigansKilled));
            int deer = Mathf.FloorToInt(StatsManager.GetValue(StatID.StagsKilled));
            int wolf = Mathf.FloorToInt(StatsManager.GetValue(StatID.WolvesKilled));
            int bear = Mathf.FloorToInt(StatsManager.GetValue(StatID.BearsKilled));
            int moose = Mathf.FloorToInt(StatsManager.GetValue(StatID.MooseKilled));
            int cougar = Mathf.FloorToInt(StatsManager.GetValue(StatID.CougarsKilled));

            return $"猎物: 兔{rabbit} 松鸡{ptarmigan} 鹿{deer} 狼{wolf} 熊{bear} 驼鹿{moose} 美洲狮{cougar}";
        }
        private void HandleDrag(Rect rect, ref bool dragging, ref float x, ref float y)
        {
            Event e = Event.current;

            if (e.type == EventType.MouseDown && rect.Contains(e.mousePosition))
            {
                dragging = true;
                dragOffset = e.mousePosition - new Vector2(x, y);
            }

            if (e.type == EventType.MouseDrag && dragging)
            {
                x = e.mousePosition.x - dragOffset.x;
                y = e.mousePosition.y - dragOffset.y;
            }

            if (e.type == EventType.MouseUp)
            {
                if (dragging)
                    Settings.options.Save();

                dragging = false;
            }
        }
        [HarmonyPatch(typeof(PlayerMovement), "Update")]
        internal static class PlayerMovement_Update_Patch
        {
            private static void Postfix(PlayerMovement __instance)
            {
                testModMain.currentSpeedMS = __instance.GetVelocity().magnitude;
            }
        }
        private string GetWeatherCN(Weather w)
        {
            if (w == null) return "--";

            return w.GetWeatherStage() switch
            {
                WeatherStage.DenseFog => "浓雾",
                WeatherStage.LightSnow => "小雪",
                WeatherStage.HeavySnow => "大雪",
                WeatherStage.PartlyCloudy => "多云",
                WeatherStage.Clear => "晴天",
                WeatherStage.Cloudy => "阴天",
                WeatherStage.LightFog => "薄雾",
                WeatherStage.Blizzard => "暴雪",
                WeatherStage.ClearAurora => "极光",
                WeatherStage.ToxicFog => "毒雾",
                WeatherStage.ElectrostaticFog => "电子雾",
                _ => "未知"
            };
        }

        private string GetWindCN(WindStrength w)
        {
            return w switch
            {
                WindStrength.Calm => "无风",
                WindStrength.SlightlyWindy => "微风",
                WindStrength.Windy => "有风",
                WindStrength.VeryWindy => "大风",
                WindStrength.Blizzard => "暴风",
                _ => "未知"
            };
        }

        //时间变速
        private void UpdateTimeScale()
        {
             if (!Settings.options.enableTimeScale) return;

            if (Input.GetKeyDown(Settings.options.timeKey))
            {

                timeActive = !timeActive;

                float scale = timeActive ? Settings.options.timeScale : 1f;

                if (Time.timeScale != scale)
                {
                    Time.timeScale = scale;
                    GameManager.m_GlobalTimeScale = scale;
                }
            }
        }

        //飞行
        private void UpdateFlyMode()
        {
            if (Settings.options.enableFly && Input.GetKeyDown(Settings.options.flyKey))
            {
                FlyMode.Enter();
            }
        }

        //伤害与播报
        // private void UpdateCombat() { }
        [HarmonyPatch(typeof(BaseAi), nameof(BaseAi.ApplyDamage), new Type[] { typeof(float), typeof(float), typeof(DamageSource), typeof(string) })]
        internal static class BaseAi_ApplyDamage_Patch
        {
            private static void Prefix(ref float damage)
            {
                damage *= Settings.options.damageScale;
            }
        }
        
        [HarmonyPatch(typeof(BaseAi), nameof(BaseAi.EnterDead))]
        internal static class BaseAi_EnterDead_Patch
        {
            private static void Prefix(BaseAi __instance)
            {
                if (!Settings.options.killFeed) return;
                if (__instance == null || __instance.m_CurrentMode == AiMode.Dead) return;



        // ✅ 排除场景加载（非常关键）
        if (Time.timeSinceLevelLoad < 3f) return;

        // ✅ 核心：只允许玩家击杀
        if (__instance.m_DamageSource != DamageSource.Player &&
            __instance.m_DamageSource != DamageSource.NoiseMaker)
            return;

                string name = __instance.gameObject?.name ?? "未知生物";
                string cnName = name switch
                {
                    var n when n.Contains("Wolf")      => "狼",
                    var n when n.Contains("Bear")      => "熊",
                    var n when n.Contains("Moose")     => "驼鹿",
                    var n when n.Contains("Doe")      => "雌鹿",
                    var n when n.Contains("Stag")      => "雄鹿",
                    var n when n.Contains("Rabbit")    => "兔子",
                    var n when n.Contains("Cougar")    => "美洲狮",
                    var n when n.Contains("Ptarmigan") => "松鸡",
                    _ => name.Replace("(Clone)", "").Trim()
                };

                string killText = $"击杀 ☠ {cnName}";

                PlayerDamageEvent.SpawnDamageEvent(
                    
                    damageEventName: killText,           // 主文字（击杀XX）
                    damageEventType: "",                 // 类型文字留空（不显示）
                    iconName: "ico_injury_foodPoisoning",    
                    tint: new Color(1f, 0.9f, 0.4f),     // 淡黄色
                    fadeout: true,
                    displayTime: 4f,                     // 显示4秒
                    fadeoutTime: 0.8f                    // 淡出0.8秒
                );
            }
        }
        //生存
        [HarmonyPatch(typeof(HUDManager), "UpdateCrosshair")]
        public class ForceCrosshairAlwaysVisible
        {
            public static void Postfix(HUDManager __instance)
            {
                if (!Settings.options.alwaysShowCrosshair) return;
                
                // 获取 Panel_HUD
                if (InterfaceManager.TryGetPanel<Panel_HUD>(out var hudPanel) && hudPanel?.m_Sprite_Crosshair != null)
                {
                    var crosshair = hudPanel.m_Sprite_Crosshair;
                    Utils.SetActive(crosshair.gameObject, true);
                    crosshair.alpha = 1f;
                }
            }
        }

        [HarmonyPatch(typeof(Sprains), "MaybeSprainWhileMoving")]
        class DisableAllSprains
        {
            static bool Prefix()
            {
                if (Settings.options.noSprain)
                {
                    return false; // 完全跳过整个扭伤逻辑
                }

                return true;
            }
        }
        [HarmonyPatch(typeof(IceCrackingTrigger), "OnTriggerEnter")]
        private static class CMOD_IceCrackingTrigger_OnTriggerEnter_Patch
        {
            private static bool Prefix()
            {
                return !Settings.options.noThinIce;
            }
        }

        [HarmonyPatch(typeof(IceCrackingWarningTrigger), "OnTriggerEnter")]
        private static class CMOD_IceCrackingWarningTrigger_OnTriggerEnter_Patch
        {
            private static bool Prefix()
            {
                return !Settings.options.noThinIce;
            }
        }

        [HarmonyPatch(typeof(IceCrackingManager), "UpdateHUDWarning")]
        private static class CMOD_IceCrackingManager_UpdateHUDWarning_Patch
        {
            private static bool Prefix()
            {
                return !Settings.options.noThinIce;
            }
        }
        //容器
        [HarmonyPatch(typeof(Container))]
        internal static class Container_Patch
        {
            [HarmonyPatch("Awake")]
            [HarmonyPostfix]
            private static void Postfix(Container __instance)
            {
                UpdateContainer(__instance);
            }

            [HarmonyPatch("Start")]
            [HarmonyPostfix]
            private static void Postfix_Start(Container __instance)
            {
                UpdateContainer(__instance);
            }

            private static void UpdateContainer(Container __instance)
            {
                if (Settings.options == null) return;

                if (Settings.options.enableContainerCapacity)
                {
                    
                var original = __instance.m_Capacity;
            var extra = ItemWeight.FromKilograms(Settings.options.containerCapacity);

            __instance.m_Capacity = original + extra;
                }
            }
        }
        //负重增加buff代码位置
[HarmonyPatch(typeof(Inventory), nameof(Inventory.GetTotalWeightKG))]
internal static class Inventory_InfiniteWeight_Patch
{
    private static void Postfix(ref ItemWeight __result)
    {
        if (!Settings.options.enableExtraCarryCapacity) return;
        
        // 直接返回0重量，实现无限负重
        __result = ItemWeight.Zero;
    }
}

        //生火
        

[HarmonyPatch(typeof(Fire), nameof(Fire.FireShouldBlowOutFromWind))]
    internal static class Fire_FireShouldBlowOutFromWind_Patch
    {
        static bool Prefix(ref bool __result)
        {
            if (Settings.options == null) return true;
            
            if (Settings.options.fireIgnoreWind)
            {
                __result = false;
                return false;
            }
            
            return true;
        }
    }

    [HarmonyPatch]
    internal static class FireplaceInteraction_TooWindyToStart_Patch
    {
        static MethodBase TargetMethod()
        {
            return AccessTools.Method(typeof(FireplaceInteraction), "TooWindyToStart");
        }

        static bool Prefix(ref bool __result)
        {
            if (Settings.options == null) return true;
            
            if (Settings.options.fireIgnoreWind)
            {
                __result = false;
                return false;
            }
            
            return true;
        }
    }

    [HarmonyPatch(typeof(Campfire), "TooWindyToStart")]
    internal static class Campfire_TooWindyToStart_Patch
    {
        static bool Prefix(ref bool __result)
        {
            if (Settings.options == null) return true;
            
            if (Settings.options.fireIgnoreWind)
            {
                __result = false;
                return false;
            }
            
            return true;
        }
    }

    [HarmonyPatch(typeof(Panel_FireStart), "HasDirectSunlight")]
    internal static class Panel_FireStart_HasDirectSunlight_Patch
    {
        static void Postfix(ref bool __result)
        {
            if (Settings.options == null) return;
            
            if (Settings.options.lensNoSun)
            {
                __result = true;
            }
        }
    }

    [HarmonyPatch(typeof(TorchItem), nameof(TorchItem.Awake))]
    internal class TorchItem_Awake_Patch
    {
        public static void Postfix(TorchItem __instance)
        {
            if (Settings.options.torchNoWindExtinguish)
            {
                __instance.m_WindSpeedExtinguish = float.MaxValue;
                __instance.m_WindSpeedCannotIgnite = float.MaxValue;
            }
        }
    }
// ===================== 火把温度增强 =====================
[HarmonyPatch(typeof(TorchItem), nameof(TorchItem.Awake))]
internal class TorchItem_HeatBoost_Patch
{
    private static void Postfix(TorchItem __instance)
    {
        if (!Settings.options.enableTorchHeat)
            return;

        if (__instance == null || __instance.m_HeatSource == null)
            return;

        // 只在火把生成时增加一次最大温度
        __instance.m_HeatSource.m_MaxTempIncrease += Settings.options.torchExtraHeat;
    }
}

    // 手电筒无限电量
   [HarmonyPatch(typeof(FlashlightItem), nameof(FlashlightItem.GetNormalizedCharge))]
   internal class FlashlightItem_GetNormalizedCharge_Patch
   {
       public static bool Prefix(ref float __result)
       {
           if (Settings.options.infiniteFlashlight)
           {
               __result = 1f;
               return false;
           }
           return true;
       }
   }

   [HarmonyPatch(typeof(FlashlightItem), nameof(FlashlightItem.Awake))]
   internal class FlashlightItem_Awake_Patch
   {
       public static void Postfix(FlashlightItem __instance)
       {
           if (Settings.options.infiniteFlashlight)
           {
               __instance.m_LowBeamDuration = float.MaxValue;
               __instance.m_HighBeamDuration = float.MaxValue;
               __instance.m_CurrentBatteryCharge = __instance.m_LowBeamDuration;
           }
       }
   }

   [HarmonyPatch(typeof(FlashlightItem), nameof(FlashlightItem.IsLit))]
   internal class FlashlightItem_IsLit_Patch
   {
       public static bool Prefix(FlashlightItem __instance, ref bool __result)
       {
           if (Settings.options.infiniteFlashlight)
           {
               __result = __instance.IsOn();
               return false;
           }
           return true;
       }
    }
    
    //烹饪————————————————————————————————————————————————————————————————————————————

// ===================== 不烧焦 =====================
// ===================== 食物不烧焦成功版本 =====================
// [HarmonyPatch(typeof(CookingPotItem))]
// internal class CookingPotItem_NoBurn_Patch
// {
//     // 每帧重置烧焦进度
//     [HarmonyPatch("Update")]
//     [HarmonyPostfix]
//     private static void Postfix_Update(CookingPotItem __instance)
//     {
//         if (Settings.options == null || !Settings.options.noBurnFood) return;
        
//         if (__instance.m_GearItemBeingCooked != null)
//         {
//             __instance.m_PercentRuined = 0f;
//             __instance.m_MinutesUntilRuined = float.MaxValue;
//             __instance.m_GracePeriodElapsedHours = 0f;
//         }
//     }
    
//     // 阻止状态变为 Ruined
//     [HarmonyPatch("SetCookingState")]
//     [HarmonyPrefix]
//     private static bool Prefix_SetCookingState(CookingPotItem __instance, dynamic cookingState)
//     {
//         if (Settings.options == null || !Settings.options.noBurnFood) return true;
        
//         int newState = (int)cookingState;
//         if (newState == 2 && __instance.m_GearItemBeingCooked != null)
//         {
//             return false;
//         }
        
//         return true;
//     }
// }


[HarmonyPatch(typeof(CookingPotItem))]
internal class CookingPotItem_NoBurn_Patch
{
    // 每帧清除烧焦/烧干进度
    [HarmonyPatch("Update")]
    [HarmonyPostfix]
    private static void Postfix_Update(CookingPotItem __instance)
    {
        if (Settings.options == null || !Settings.options.noBurnFood)
            return;

        // 是否有食物
        bool hasFood =
            __instance.m_GearItemBeingCooked != null;

        // 是否有正在烧的水
        bool hasWater =
            __instance.m_LitersWaterBeingBoiled >
            ItemLiquidVolume.Zero;

        // 是否有正在融化的雪
        bool hasSnow =
            __instance.m_LitersSnowBeingMelted >
            ItemLiquidVolume.Zero;

        if (hasFood || hasWater || hasSnow)
        {
            // 锁定不烧焦
            __instance.m_PercentRuined = 0f;

            // 永不进入烧焦倒计时
            __instance.m_MinutesUntilRuined = float.MaxValue;

            // 重置宽限期
            __instance.m_GracePeriodElapsedHours = 0f;
        }
    }

    // 阻止进入 Ruined 状态
   [HarmonyPatch("SetCookingState")]
    [HarmonyPrefix]
    private static bool Prefix_SetCookingState(
        CookingPotItem __instance,
        int cookingState)
    {
        if (Settings.options == null || !Settings.options.noBurnFood)
            return true;

        // 2 = Ruined
        if (cookingState == 2)
        {
            // 强制保持 Ready
            __instance.m_CookingState =
                (CookingPotItem.CookingState)1;

            return false;
        }

        return true;
    }
}

[HarmonyPatch(typeof(BodyHarvest))]
internal class BodyHarvest_RoundMeatWeight_Patch
{
    [HarmonyPatch("InitializeResourcesAndConditions")]
    [HarmonyPostfix]
    private static void Postfix_InitializeResourcesAndConditions(
        BodyHarvest __instance)
    {
        if (Settings.options == null ||
            !Settings.options.noRuinedMeat)
        {
            return;
        }

        if (__instance == null)
        {
            return;
        }

       
            // 当前初始化后的肉量
            float kg =
                __instance.m_MeatAvailableKG /
                ItemWeight.FromKilograms(1f);

            float roundedKg =
                Mathf.Ceil(kg);

            // 已经是整数
            if (Mathf.Approximately(
                kg,
                roundedKg))
            {
                return;
            }

            // MelonLogger.Msg(
            //     $"[碎肉修正] 初始化肉量: {kg:F2}kg -> {roundedKg:F0}kg"
            // );

            // 改成整数
            __instance.m_MeatAvailableKG =
                ItemWeight.FromKilograms(
                    roundedKg);
        
    }
}

[HarmonyPatch(typeof(Panel_BodyHarvest))]
internal class HarvestTimeMultiplierPatch
{
    [HarmonyPatch("GetHarvestDurationMinutes")]
    [HarmonyPostfix]
    private static void Postfix(
        ref float __result)
    {
        if (Settings.options == null)
        {
            return;
        }

        __result *=
            Settings.options.harvestTimeMultiplier;
    }
}

    //钓鱼改
    [HarmonyPatch(typeof(IceFishingHole), "Awake")]
	internal static class IceFishingHole_Awake_Patch
	{
		private static float origMin, origMax;
		private static bool cached = false;

		private static void Postfix(IceFishingHole __instance)
		{
			if (!cached)
			{
				origMin = __instance.m_MinGameMinutesCatchFish;
				origMax = __instance.m_MaxGameMinutesCatchFish;
				cached = true;
			}

			if (Settings.options == null)
				return;
            // MelonLogger.Msg($"[钓鱼日志] 最小时间: {__instance.m_MinGameMinutesCatchFish}, 最大时间: {__instance.m_MaxGameMinutesCatchFish}");
			float mul = Settings.options.fishingTimeMultiplier;
			// __instance.m_MinGameMinutesCatchFish = origMin * mul;
			// __instance.m_MaxGameMinutesCatchFish = origMax * mul;
            __instance.m_MinGameMinutesCatchFish *=   mul;
			__instance.m_MaxGameMinutesCatchFish *=   mul;
            // MelonLogger.Msg($"[钓鱼日志2] 最小时间: {__instance.m_MinGameMinutesCatchFish}, 最大时间: {__instance.m_MaxGameMinutesCatchFish}");
		}
	}

   
        
    }
}