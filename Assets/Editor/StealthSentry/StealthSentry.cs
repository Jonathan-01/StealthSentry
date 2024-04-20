using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace StealthSentry.Overview
{
    public class StealthSentry : EditorWindow
    {
        [MenuItem("StealthSentry/Overview", false, 1)]
        public static void ShowWindow()
        {
            StealthSentry wnd = GetWindow<StealthSentry>();

            wnd.titleContent = new GUIContent("StealthSentry");

            wnd.minSize = new Vector2(480, 70);
        }

        public void CreateGUI()
        {
            // Each editor window contains a root VisualElement object
            VisualElement root = rootVisualElement;
            root.styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/StealthSentry/Styles/MainStyle.uss"));

            VisualElement scrollContainer = new ScrollView();

            #region inside scrollContainer
            // Welcome message. Still a WIP but should be present in some form
            VisualElement introMessage = new Label("Welcome to the Stealth Sentry tool!\n" +
                "This tool is intended to be opensource and provide an easy to understand base functionality for stealth gameplay mechanics. Included in the tool is an example " +
                "player controller which is not reccomended for use and there only to serve as an example or to built on. This page will provide an overview of the included " +
                "features as well as a short introduction to their code.");
            introMessage.AddToClassList("body");
            scrollContainer.Add(introMessage);

            // Display the logo
            // May be changed to a watermark later
            VisualElement logoBanner = new Image() { image = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Editor/StealthSentry/Images/LogoWIP.png") };
            logoBanner.AddToClassList("image-banner");
            scrollContainer.Add(logoBanner);

            // Features heading
            VisualElement featuresHeading = new Label("\nFeatures");
            featuresHeading.AddToClassList("heading");
            scrollContainer.Add(featuresHeading);

            // Update
            #region enemies foldout
            VisualElement enemyFoldout = new Foldout() { text = "Enemy AI", value = false };
            enemyFoldout.AddToClassList("subheading");

            // Create a text label
            VisualElement enemyLabel = new Label("These are enemy AI you can add to the scene. They come with vision cones, patrol routes and hearing.");
            enemyLabel.AddToClassList("body");
            enemyFoldout.Add(enemyLabel);

            // Display a relevant picture of the graph
            VisualElement enemyPic = new Image() { image = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Editor/StealthSentry/Images/EnemyPrefab.jpg") };
            enemyPic.AddToClassList("image-large");
            enemyFoldout.Add(enemyPic);

            // Write brief instructions on each part
            VisualElement enemyBody = new Label("The enemy prefab makes use of the EnemyController.cs script and navmesh funtionality to have an enemy that can patrol and " +
                "sense the player. There are gizmos attached to help visualise the AI's senses and patrol route. \n\n" +

                "In blue you can see the vision range of of the enemy represented by 3 lines with the middle line pointing along the enemies forward. Enemies will cast a ray" +
                " looking for the player so long as they are within range. If the player is crouched then the ray will be cast from a lower point to allow cover behind short" +
                " objects in the scene.\n" +
                "In yellow a wireframe sphere represents the hearing range of an enemy. If the player is crouched or stationary then the enemy will not hear them, otherwise the" +
                " enemy will be able to detect player even through walls and move to investigate.\n" +
                "In magenta a route is drawn between the patrol points assigned to the enemy. A grey line will also be drawn between the enemy and the first patrol point.\n\n" +

                "The code behind the enemy controller is a finite state machine AI that uses a state enumerator do decide what logic and actions it should be doing at " +
                "any given point in time. This means that if you want to change how the enemy acts or thinks you simply need to follow the logic of 'if it sees a player while " +
                "chasing then it should change to the shooting state. When in the shooting state it should stop shooting and return to chasing if the player leaves line of sight" +
                " for a given amount time'. Approaching the task with a clear idea of how it should act and the conditions to change how it acts will help to visualise and" +
                " implement new code.\n" +
                "The code also contains some barebones functionality for an animator component should it be attached. The given code will allow for the enemy controller to pass " +
                "a value for whether the enemy is moving. This could be used to switch between an idle and walking animation should you implement it.\n" +
                "There are also logic operations for hearing and seeing a player. These currently use a reference to the provided player controller to find whether the player is " +
                "crouched or moving. If you integrate your own player controller (which would be recommended) then please update this reference and make sure to include similar " +
                "variables within the integrated player controller.");
            enemyBody.AddToClassList("body");
            enemyFoldout.Add(enemyBody);

            scrollContainer.Add(enemyFoldout);
            #endregion

            #region hiding spots foldout
            VisualElement hidingSpotsFoldout = new Foldout() { text = "Hiding Spots", value = false };
            hidingSpotsFoldout.AddToClassList("subheading");

            // Create a text label
            VisualElement hidingSpotsLabel = new Label("Hiding spots and cover allow for planned safe areas a player can return to or use to avoid enemies.");
            hidingSpotsLabel.AddToClassList("body");
            hidingSpotsFoldout.Add(hidingSpotsLabel);

            // Display a relevant picture of the graph
            VisualElement hidingSpotsPic = new Image() { image = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Editor/StealthSentry/Images/HidingPrefab.jpg") };
            hidingSpotsPic.AddToClassList("image-large");
            hidingSpotsFoldout.Add(hidingSpotsPic);

            // Write brief instructions on each part
            VisualElement hidingSpotsBody = new Label("The hiding spot prefab that has been included allows a player to interact and hide. It contains a hitbox to" +
                " detect the player and a camera to switch to when hidden. If you intend to make any hiding spots of your own please reference the player controller" +
                " included.\n" +
                "The provided cover prefab is made to be the correct height for the enemy AI to not see the player if they are crouching. The enemy controller is made" +
                " to be usable with any kind of obstacle so feel free to experiment. If you wish to change how the enemy finds the player to allow for shorter cover then " +
                "plaese refer to the SeenPlayer function inside EnemyController.cs.");
            hidingSpotsBody.AddToClassList("body");
            hidingSpotsFoldout.Add(hidingSpotsBody);

            scrollContainer.Add(hidingSpotsFoldout);
            #endregion

            #region Alarms
            VisualElement alarmFoldout = new Foldout() { text = "Alarms", value = false };
            alarmFoldout.AddToClassList("subheading");

            // Create a text label
            VisualElement alarmLabel = new Label("The alarm is mainly composed of the Alarm.cs script. The included examples are an alarm hitbox and an interactable" +
                " object that triggers an alarm.");
            alarmLabel.AddToClassList("body");
            alarmFoldout.Add(alarmLabel);

            // Display a relevant picture of the graph
            VisualElement alarmPic = new Image() { image = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Editor/StealthSentry/Images/AlarmPrefab.jpg") };
            alarmPic.AddToClassList("image-large");
            alarmFoldout.Add(alarmPic);

            // Write brief instructions on each part
            VisualElement alarmBody = new Label("The alarms allow for enemies within a set area to be alerted should a player trigger it. Upon triggering the alarm " +
                "enemies within the set range will be changed to an alert state and move to investigate the position of the object that had the alarm attached. This" +
                " range is shown by the yellow wireframe sphere and is centred on the asset with the alarm script or the referenced audio source component within the" +
                " scene.\n\n" +

                "The code contains simple functions that will set all enemies within range to Alert state if the are not currently in pursuit of the player. It will " +
                "attempt to do this each update for the duration of the alarm. To do this it uses the AlertEnemy function in the enemy controller so make sure to update " +
                "that function if you wish to add any new enemy states that you don't want to be interrupted by an alarm.");
            alarmBody.AddToClassList("body");
            alarmFoldout.Add(alarmBody);

            scrollContainer.Add(alarmFoldout);
            #endregion

            // WIP heading
            VisualElement WIPHeading = new Label("\nWork in Progress [WIP]");
            WIPHeading.AddToClassList("heading");
            scrollContainer.Add(WIPHeading);

            #region behaviour graph foldout
            VisualElement behaviourGraphFoldout = new Foldout() { text = "Behavioral Graph", value = false };
            behaviourGraphFoldout.AddToClassList("subheading");

            // Create a text label
            VisualElement behaviousGraphLabel = new Label("This is the behavioural graph that assists in creating AI behaviours. " +
                "It uses a graph interface to allow you to connect nodes in order to create behaviours for AI.");
            behaviousGraphLabel.AddToClassList("body");
            behaviourGraphFoldout.Add(behaviousGraphLabel);

            // Display a relevant picture of the graph
            VisualElement behaviouralGraphPic = new Image() { image = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Editor/StealthSentry/Images/WIP.png") };
            behaviouralGraphPic.AddToClassList("image-large");
            behaviourGraphFoldout.Add(behaviouralGraphPic);

            // Write brief instructions on each part
            VisualElement behaviourGraphBody = new Label("Currently not implemented but you will be able to right-click within the graph to create nodes. " +
                "These will consist of a start node, decision based nodes (such as if-statements) and reactionary nodes (such as changing state to a different behaviour" +
                "or moving to set points). These nodes will include basic functions as well as modularity to allow for custom scripts to be added and performed." +
                "These modular nodes will need to be able to read the provided script to determine what data will be passed in and how many different outputs it will have. " +
                "\n\nUnder this brief explanation there will be a quick guide on how to use the graph. This guide will use gifs to help communicate how each piece works." +
                "While this isn't a neccesary feature it is likely that it would be help to speed-up the learning process. I may also add a button somewhere to go to a " +
                "quick reference page to assist with troubleshooting where required.");
            behaviourGraphBody.AddToClassList("body");
            behaviourGraphFoldout.Add(behaviourGraphBody);

            scrollContainer.Add(behaviourGraphFoldout);
            #endregion

            #region noise and distractions foldout
            VisualElement noiseFoldout = new Foldout() { text = "Noise and Distractions", value = false };
            noiseFoldout.AddToClassList("subheading");

            // Create a text label
            VisualElement noiseLabel = new Label("This will be different depending on the desirability.");
            noiseLabel.AddToClassList("body");
            noiseFoldout.Add(noiseLabel);

            // Display a relevant picture of the graph
            VisualElement noisePic = new Image() { image = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Editor/StealthSentry/Images/WIP.png") };
            noisePic.AddToClassList("image-large");
            noiseFoldout.Add(noisePic);

            // Write brief instructions on each part
            VisualElement noiseBody = new Label("Based on the desirability from the user survey this will either just be placable spots or will also include" +
                " light detection. This would allow the user to have darker areas that a player could be more hidden inside of. It could also include a painting " +
                "tool to allow a user to paint areas where a player will be hidden. This would make it a lot more versatile but shouldn't be the whole tool as it " +
                "would likely prove tedious to have to manually paint where a player would be hidden.");
            noiseBody.AddToClassList("body");
            noiseFoldout.Add(noiseBody);

            scrollContainer.Add(noiseFoldout);
            #endregion

            #region AI Incapacitation
            VisualElement incapFoldout = new Foldout() { text = "AI Incapacitation", value = false };
            incapFoldout.AddToClassList("subheading");

            // Create a text label
            VisualElement incapLabel = new Label("This will be attachable to AI to allow for them to be incapacitated.");
            incapLabel.AddToClassList("body");
            incapFoldout.Add(incapLabel);

            // Display a relevant picture of the graph
            VisualElement incapPic = new Image() { image = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Editor/StealthSentry/Images/WIP.png") };
            incapPic.AddToClassList("image-large");
            incapFoldout.Add(incapPic);

            // Write brief instructions on each part
            VisualElement incapBody = new Label("Based on desirability this will be a basic health counter and will include a player component to allow for " +
                "attacks to be made. If indicated to be especially desirable it will also include stealth takedowns and drop takedowns which will show an " +
                "indicator when performable.");
            incapBody.AddToClassList("body");
            incapFoldout.Add(incapBody);

            scrollContainer.Add(incapFoldout);
            #endregion

            #region AI Communication
            VisualElement commFoldout = new Foldout() { text = "AI Communcation", value = false };
            commFoldout.AddToClassList("subheading");

            // Create a text label
            VisualElement commLabel = new Label("This will be attachable to AI to allow them to communicate between eachother.");
            commLabel.AddToClassList("body");
            commFoldout.Add(commLabel);

            // Display a relevant picture of the graph
            VisualElement commPic = new Image() { image = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Editor/StealthSentry/Images/WIP.png") };
            commPic.AddToClassList("image-large");
            commFoldout.Add(commPic);

            // Write brief instructions on each part
            VisualElement commBody = new Label("This will include a proximity for AI talking as well as a toggle for global communication to allow some enemies to" +
                " spread information regardless of distance. If marked as high desirablitily this will also include radio towers or something similar that would " +
                "allow for the gloabl communication to be disabled through player input.");
            commBody.AddToClassList("body");
            commFoldout.Add(commBody);

            scrollContainer.Add(commFoldout);
            #endregion

            #region Guard Placement
            VisualElement guardPlacementFoldout = new Foldout() { text = "Procedural Guard Placement", value = false };
            guardPlacementFoldout.AddToClassList("subheading");

            // Create a text label
            VisualElement guardPlacementLabel = new Label("These will be a placable volume that will have parameters to generate a set number of guards.");
            guardPlacementLabel.AddToClassList("body");
            guardPlacementFoldout.Add(guardPlacementLabel);

            // Display a relevant picture of the graph
            VisualElement guardPlacementPic = new Image() { image = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Editor/StealthSentry/Images/WIP.png") };
            guardPlacementPic.AddToClassList("image-large");
            guardPlacementFoldout.Add(guardPlacementPic);

            // Write brief instructions on each part
            VisualElement guardPlacementBody = new Label("The type(s) and number of guards will need to be specified by the user and algorithmically guards will be placed" +
                " to cover the most of the given volumes floor space. This will include the specification whether thes guards should be wall-mounted, grounded or flying.");
            guardPlacementBody.AddToClassList("body");
            guardPlacementFoldout.Add(guardPlacementBody);

            scrollContainer.Add(guardPlacementFoldout);
            #endregion

            #region Difficutly Visualisation
            VisualElement diffFoldout = new Foldout() { text = "Difficutly Visualisation", value = false };
            diffFoldout.AddToClassList("subheading");

            // Create a text label
            VisualElement diffLabel = new Label("This will be a shader that will show the most patrolled and viewed areas.");
            diffLabel.AddToClassList("body");
            diffFoldout.Add(diffLabel);

            // Display a relevant picture of the graph
            VisualElement diffPic = new Image() { image = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Editor/StealthSentry/Images/WIP.png") };
            diffPic.AddToClassList("image-large");
            diffFoldout.Add(diffPic);

            // Write brief instructions on each part
            VisualElement diffBody = new Label("The shader will be turned on in editor but executed during runtime to highlight areas as they are viewed. This " +
                "will allow the AI vision cones to highlight the areas they are viewing with these highlights slowly fading until re-highlighted. This will serve " +
                "as a visual indication of busy areas and quiet areas and potentially suggest player routes.");
            diffBody.AddToClassList("body");
            diffFoldout.Add(diffBody);

            scrollContainer.Add(diffFoldout);
            #endregion

            #region Group Behaviour
            VisualElement groupFoldout = new Foldout() { text = "Group Behaviours", value = false };
            groupFoldout.AddToClassList("subheading");

            // Create a text label
            VisualElement groupLabel = new Label("This will allow for Ai to be grouped and behave together.");
            groupLabel.AddToClassList("body");
            groupFoldout.Add(groupLabel);

            // Display a relevant picture of the graph
            VisualElement groupPic = new Image() { image = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Editor/StealthSentry/Images/WIP.png") };
            groupPic.AddToClassList("image-large");
            groupFoldout.Add(groupPic);

            // Write brief instructions on each part
            VisualElement groupBody = new Label("Grouping AI will allow them to realise when another memeber of the group is alerted or missing. It will also " +
                "allow them to communicate more effectively together.");
            groupBody.AddToClassList("body");
            groupFoldout.Add(groupBody);

            scrollContainer.Add(groupFoldout);
            #endregion

            //// Create a new button
            //VisualElement testButton = new Button();
            //VisualElement testButtonIcon = new Image(){image = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Editor/StealthSentry/Images/Icons/Arrow.png")};
            //testButtonIcon.AddToClassList("icon"); 
            //testButton.Add(testButtonIcon);

            //// Give a stylised class
            //testButton.AddToClassList("button-small");

            //// Add the button to the root element
            //scrollContainer.Add(testButton);
            #endregion

            // Add the scrollable container to the root
            root.Add(scrollContainer);
        }
    }
}