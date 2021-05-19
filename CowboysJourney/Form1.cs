using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Media;

/** Cowboy Journey - 2021-05-18 - Ted Angus
 * Flow chart completed around 11:30 5/17
 * Game started around 12:30 5/17
 * Game code 99% completed around 6:30 5/17
 * Graphics and polishing completed around 8:30 5/18
 * 
 * Statstics:
 * 38 Different Scenes
 * 84 Different Images (Counting fast horse variants)
 * Counting computer decisions, 'Horsepower' takes the most decisions to get with 10 decisions (8 player and 2 computer)
 * Counting computer decisions, and not counting luck, 'Lucky Shot' and 'Quick Shot Winner' both take the least amount of decisions at 4.
 * 
 * 15 Decisions
 * 13 Decisions with at least 2 buttons
 * 8 Decisions with at least 3 buttons
 * 1 Decision with all 4 buttons
 * 
 * It is possible to win by only clicking the first button
 * You will not win by only clicking the second button
 * You cannot only click the third or fourth buttons to win
 * It is a 5% chance for you to get the Lucky Shot ending first try.
 * It is a 21% chance for you to get Lone Survivor
*/

namespace CowboysJourney
{
    public partial class Form1 : Form
    {

        // Main Variables
        string scene = "1";
        bool fastHorse = false; // Did the player select the fast horse on the Stables route?
        int cracksInTheBridge = 0;

        // Ending variables (Used to calculate how many endings the player has gotten)
        bool endingA = false; // Has the player gotten Quick Shot Winner?
        bool endingB = false; // Has the player gotten True Cowboy?
        bool endingB2 = false; // Has the player gotten Horsepower?
        bool endingC = false; // Has the player gotten The Negotiator?
        bool endingC2 = false; // Has the player gotten Survivor?
        bool endingD = false; // Has the player gotten Lucky Shot?

        // Sound-Players
        SoundPlayer splashSound = new SoundPlayer(Properties.Resources.Splashdown);
        SoundPlayer rifleSound = new SoundPlayer(Properties.Resources.Rifle_Fire);

        // Random Generator
        Random randGen = new Random();

        public Form1()
        {
            InitializeComponent();
            updateGameDisplay(); // Start off by updating the game display
        }
        public int calculateEndings()
        {
            /** calculateEndings();
             * Used to calculate how many endings the player has reached.
             * Goes through the 6 ending variables and increases the amount of endings the player has gotten
             * Returns an integer that can be used in a string for example.
            */

            int endingsGotten = 0;
            if (endingA)
            {
                endingsGotten++;
            }
            if (endingB)
            {
                endingsGotten++;
            }
            if (endingB2)
            {
                endingsGotten++;
            }
            if (endingC)
            {
                endingsGotten++;
            }
            if (endingC2)
            {
                endingsGotten++;
            }
            if (endingD)
            {
                endingsGotten++;
            }
            return endingsGotten;
        }

        public void buttonGeneration(string button1, string button2, string button3, string button4)
        {
            /** buttonGeneration();
             * A function with four parameters for each button.
             * Decides which buttons to display depending on how many buttons are inputted.
             * Updates every button's label.
             * Can also hide every button if needed.
            */

            optionButtonA.Text = button1;
            optionButtonB.Text = button2;
            optionButtonC.Text = button3;
            optionButtonD.Text = button4;
            if (button1 == "") // Does the first button not contain text? Hide all the buttons.
            {
                optionButtonA.Visible = false;
                optionButtonB.Visible = false;
                optionButtonC.Visible = false;
                optionButtonD.Visible = false;
            }
            else if (button2 == "") // Does the second button not contain text? Hide all the buttons but one.
            {
                optionButtonA.Visible = true;
                optionButtonB.Visible = false;
                optionButtonC.Visible = false;
                optionButtonD.Visible = false;
            }
            else if (button3 == "") // Does the third button not contain text? Hide all the but the first two.
            {
                optionButtonA.Visible = true;
                optionButtonB.Visible = true;
                optionButtonC.Visible = false;
                optionButtonD.Visible = false;
            }
            else if (button4 == "") // Does the last button not contain text? Show all the buttons but one.
            {
                optionButtonA.Visible = true;
                optionButtonB.Visible = true;
                optionButtonC.Visible = true;
                optionButtonD.Visible = false;
            }
            else // If all the buttons contained text, show them all.
            {
                optionButtonA.Visible = true;
                optionButtonB.Visible = true;
                optionButtonC.Visible = true;
                optionButtonD.Visible = true;
            }
        }

        public void updateGameDisplay()
        {
            /** updateGameDisplay();
             * Uses switch and case statements depending on the scene to decide which labels, buttons, functions to run, and images to display
             * Can and will often have multiple scenes in a scene.
             * Has the 'Game Over' (represented as G) and 'Win' (represented as 'F') statements at the end.
             * The largest function by a good margin
             * 
             * Scene Groupings: (# any number)
             * 1 - The beginning scene, will split to either A# or C#.
             * A# - Towards the rifle path, can split to B#.
             * B# - Towards the horse-related paths, can split to D#.
             * C# - Towards the indoor paths, can path back towards A1.
             * D# - Towards the bullet path.
             * 
             * Larger scenes will have comments on them, the rest are pretty self explanatory
             * 1. Update the top label above the image
             * 2. Update the image to it's required image
             * 2b. Occassionally there will be a sound here if necessary
             * 3. Update the question label below the image
             * 4. Update what the buttons say, and what buttons are shown with a function described higher up
            */
            switch (scene)
            {
                case "1":
                    dialogueText.Text = "You're inside a wooden building, you had just gotten your 'Cowboy License' a few minutes earlier. It allows you to fully use your rifle as needed. You are now able to leave, or you could stay put.";
                    dialogueImage.Image = Properties.Resources.Scene1;
                    questionLabel.Text = "Where to?";
                    buttonGeneration("Out the door", "Stay put", "", "");
                    break;
                case "A1":
                    dialogueText.Text = "You head outside the building, All the buildings were made of wooden beams and planks. It was peaceful in town. Out in the distance, you could spot a few figures (maybe bandits?) in the distance, approaching the town.";
                    dialogueImage.Image = Properties.Resources.SceneA1;
                    questionLabel.Text = "What now?";
                    buttonGeneration("Head towards them", "Prepare", "", "");
                    break;
                case "A2":
                    dialogueText.Text = "You could easily tell who they were: The bandits! The leader challenges you to a quick draw. If you win, they leave and you gain some cash. If you lose, they take everything.";
                    dialogueImage.Image = Properties.Resources.SceneA2;
                    questionLabel.Text = "Quick Draw?";
                    buttonGeneration("Yes", "No", "", "");
                    break;
                case "A3":
                    dialogueText.Text = "You declined the quick draw, the bandits were saddened, so they shot you for not partaking.";
                    dialogueImage.Image = Properties.Resources.SceneA3;
                    scene = "G";
                    break;
                case "A4":
                    // Update text and hide the buttons/question
                    dialogueText.Text = "You accepted their challenge, both of you hold your guns down, eyes closed.";
                    dialogueImage.Image = Properties.Resources.SceneA4_Pre;
                    questionLabel.Text = "";
                    buttonGeneration("", "", "", "");

                    // Wait 1.5 seconds, before changing the image to black
                    this.Refresh();
                    Thread.Sleep(1500);
                    dialogueImage.Image = Properties.Resources.SceneA4_Pre2;

                    // Wait another 1.5 seconds, decide how well the quick shot goes with a random generator, and play a rifle sound
                    this.Refresh();
                    Thread.Sleep(1500);
                    int quickShotCheck = randGen.Next(1, 21);
                    rifleSound.Play();
                    Thread.Sleep(100);
                    if (quickShotCheck <= 5) // They win by far, update the image, play a farther seperated rifle sound
                    {
                        dialogueText.Text = "They shoot.";
                        dialogueImage.Image = Properties.Resources.SceneA4_TWBF;
                        rifleSound.Play();
                        Thread.Sleep(200);
                        rifleSound.Play();
                    }
                    else if (quickShotCheck <= 9) // They win barely, update the image, play a lesser seperated rifle sound
                    {
                        dialogueText.Text = "You shoot.";
                        dialogueImage.Image = Properties.Resources.SceneA4_TWB;
                        rifleSound.Play();
                        Thread.Sleep(100);
                        rifleSound.Play();
                    }
                    else if (quickShotCheck <= 11) // Draw, update the image, play double the rifle sounds
                    {
                        dialogueText.Text = "You both shoot.";
                        dialogueImage.Image = Properties.Resources.SceneA4_YBS;
                        rifleSound.Play();
                        rifleSound.Play();
                    }
                    else if (quickShotCheck <= 15) // You win barely, update the image, play a lesser seperated rifle sound
                    {
                        dialogueText.Text = "They shoot.";
                        dialogueImage.Image = Properties.Resources.SceneA4_YWB;
                        rifleSound.Play();
                        Thread.Sleep(100);
                        rifleSound.Play();
                    }
                    else // You win by far, update the image, play a farther seperated rifle sound
                    {
                        dialogueText.Text = "You shoot.";
                        dialogueImage.Image = Properties.Resources.SceneA4_YWBF;
                        rifleSound.Play();
                        Thread.Sleep(200);
                        rifleSound.Play();
                    }

                    // Wait 3 seconds before showcasing who the winner actually is.
                    this.Refresh();
                    Thread.Sleep(3000);

                    // Update the image accordingly depending on the random generator from earlier to match it up with the statements from earlier.
                    if (quickShotCheck <= 5) // They win by far
                    {
                        dialogueText.Text = "And you died. Too bad!";
                        dialogueImage.Image = Properties.Resources.SceneA4_TWBF2;
                        scene = "G";
                    }
                    else if (quickShotCheck <= 9) // They win barely
                    {
                        dialogueText.Text = "But they shot first, saddening.";
                        dialogueImage.Image = Properties.Resources.SceneA4_TWB2;
                        scene = "G";
                    }
                    else if (quickShotCheck <= 11) // Draw
                    {
                        dialogueText.Text = "And you both died, unfortunate!";
                        dialogueImage.Image = Properties.Resources.SceneA4_YBS2;
                        scene = "G";
                    }
                    else if (quickShotCheck <= 15) // You win barely, unlock the ending if the user wins barely or by far.
                    {
                        dialogueText.Text = "But you shot first, good job.";
                        dialogueImage.Image = Properties.Resources.SceneA4_YWB2;
                        endingA = true;
                        scene = "F";
                    }
                    else // You win by far
                    {
                        dialogueText.Text = "And you won! Great work.";
                        dialogueImage.Image = Properties.Resources.SceneA4_YWBF2;
                        endingA = true;
                        scene = "F";
                    }
                    break;
                case "B1":
                    dialogueText.Text = "You can only make it to one of these places.";
                    dialogueImage.Image = Properties.Resources.SceneB1;
                    questionLabel.Text = "Where do you gear up?";
                    buttonGeneration("The Bar", "The Stables", "The Rifle Shop", "");
                    break;
                case "B2":
                    dialogueText.Text = "You went to the bar, drank some ale, felt stronger than ever!";
                    dialogueImage.Image = Properties.Resources.SceneB2_A;
                    questionLabel.Text = "";
                    buttonGeneration("", "", "", "");

                    // Wait 2 seconds before displaying another few labels and image.
                    this.Refresh();
                    Thread.Sleep(2000);
                    dialogueText.Text += "\n\nThen you fainted. Ale ain't for the faint of heart!";
                    dialogueImage.Image = Properties.Resources.SceneB2_B;
                    scene = "G";
                    break;
                case "B3":
                    dialogueText.Text = "You head westward, to the nearby stables. However, to get to the stables, you need to cross a creaky bridge suspended over a rapid flowing river.";
                    dialogueImage.Image = Properties.Resources.SceneB3;
                    questionLabel.Text = "Onwards?";
                    buttonGeneration("Onwards", "", "", "");
                    break;
                case "B4":
                    dialogueText.Text = "You walk across the bridge...\n";
                    dialogueImage.Image = Properties.Resources.SceneB4_Start;
                    questionLabel.Text = "";
                    buttonGeneration("", "", "", "");

                    // Wait 1 second
                    this.Refresh();
                    Thread.Sleep(1000);

                    // Set up a variable to hold the crack formation chance value.
                    int crackForm = 0;
                    for (int i = 0; i < 20; i++)
                    { // For 20 steps, wait 0.3 seconds, display the right walking image depending on the remainder.
                        this.Refresh();
                        Thread.Sleep(300);
                        if (i % 2 == 0)
                        {
                            dialogueImage.Image = Properties.Resources.SceneB4_Walk1;
                        }
                        else
                        {
                            dialogueImage.Image = Properties.Resources.SceneB4_Walk2;
                        }

                        // Randomly generate a possible crack
                        crackForm = randGen.Next(1, 11);
                        if (crackForm == 1)
                        { // Add to the amount of cracks in the bridge, display text accordingly, if the user hits 3 cracks, kill them and make them start over.
                            cracksInTheBridge++;
                            if (cracksInTheBridge == 1)
                            {
                                dialogueText.Text += "Crack, ";
                                dialogueImage.Image = Properties.Resources.SceneB4_Crack;
                            }
                            else if (cracksInTheBridge == 2)
                            {
                                dialogueText.Text += "Crack! ";
                                dialogueImage.Image = Properties.Resources.SceneB4_Crack;
                            }
                            else if (cracksInTheBridge == 3)
                            {
                                dialogueText.Text += "SMASH! ";
                                dialogueImage.Image = Properties.Resources.SceneB4_Fall;

                                // Wait 1 second
                                this.Refresh();
                                Thread.Sleep(1000);
                                dialogueText.Text = "You fell in the river and got swept away.";
                                splashSound.Play();
                                questionLabel.Text = "Play Again?";
                                buttonGeneration("Yes", "No", "", "");
                                scene = "G";
                                return;
                            }
                        }
                        else
                        {
                            // 'Step' text if the random generator isn't a crack.
                            dialogueText.Text += "Step, ";
                        }
                    }
                    // Wait another 0.3 seconds before reaching the end
                    this.Refresh();
                    Thread.Sleep(300);
                    dialogueImage.Image = Properties.Resources.SceneB4_End;

                    // Wait a second to let the user see that they crossed all the way, and update with the images, labels, and buttons about the three horses.
                    this.Refresh();
                    Thread.Sleep(1000);
                    dialogueImage.Image = Properties.Resources.SceneB4_End2;
                    dialogueText.Text = "You made it across safely. You have arrived at the stables.";
                    questionLabel.Text = "What will you take?";
                    buttonGeneration("The loyal steed", "The fast but weak steed", "A slow but hard-hitting steed", "");
                    break;
                case "B6":
                    dialogueText.Text = "You take the loving and loyal steed, and you go across the bridge again.";
                    dialogueImage.Image = Properties.Resources.SceneB6;
                    questionLabel.Text = "Onwards?";
                    buttonGeneration("Onwards.", "", "", "");
                    break;
                case "B7":
                    dialogueText.Text = "You take the fast steed, and you go across the bridge again.";
                    dialogueImage.Image = Properties.Resources.SceneB7;
                    questionLabel.Text = "Onwards?";
                    buttonGeneration("Onwards.", "", "", "");
                    break;
                case "B8":
                    dialogueText.Text = "It wasn't hard-hitting for nothing, as when you tried to ride it, it rammed you out of the stable, giving you a permanent injury, and death.";
                    dialogueImage.Image = Properties.Resources.SceneB8;
                    scene = "G";
                    break;
                case "B9":
                    dialogueText.Text = "You trot across the bridge with your steed.\n";
                    dialogueImage.Image = Properties.Resources.SceneB9_Start;
                    if (fastHorse) // If you see an if statement with fast horse, it usually means that it changes the image based on the horse you picked.
                    {
                        dialogueImage.Image = Properties.Resources.SceneB9_Start_FH;
                    }
                    questionLabel.Text = "";
                    buttonGeneration("", "", "", "");
                    int maxSteps = fastHorse ? 10 : 15; // The fast horse gets to walk less steps
                    for (int j = 0; j < maxSteps; j++)
                    { // Similar function to 'B4', but cracks are more likely, and there are less steps
                        this.Refresh();
                        Thread.Sleep(300);
                        crackForm = randGen.Next(1, 21);
                        dialogueImage.Image = Properties.Resources.SceneB9_Trot;
                        if (fastHorse)
                        {
                            dialogueImage.Image = Properties.Resources.SceneB9_Trot_FH;
                        }
                        if (j % 2 == 0) // A remainder?
                        {
                            dialogueImage.Image = Properties.Resources.SceneB9_Trot2;
                            if (fastHorse)
                            {
                                dialogueImage.Image = Properties.Resources.SceneB9_Trot2_FH;
                            }
                        }
                        if (crackForm <= 3)
                        {
                            // Increase the amount of cracks, update the image with the jumping image, if there's only 1-2 cracks, it's not an issue.
                            cracksInTheBridge++;
                            dialogueImage.Image = Properties.Resources.SceneB9_Crack1;
                            if (fastHorse)
                            {
                                dialogueImage.Image = Properties.Resources.SceneB9_Crack1_FH;
                            }
                            if (cracksInTheBridge == 1)
                            {
                                dialogueText.Text += "Crack, ";
                            }
                            else if (cracksInTheBridge == 2)
                            {
                                dialogueText.Text += "Crack! ";
                            }
                            else if (cracksInTheBridge == 3)
                            {
                                // If there are three cracks, stop the user from progressing farther on the bridge
                                dialogueText.Text += "SMASH! ";
                                dialogueImage.Image = Properties.Resources.SceneB9_Fall1;
                                if (fastHorse)
                                {
                                    dialogueImage.Image = Properties.Resources.SceneB9_Fall1_FH;
                                }

                                // Wait 1 second before updating the image and label again.
                                this.Refresh();
                                Thread.Sleep(1000);
                                dialogueImage.Image = Properties.Resources.SceneB9_Fall2;
                                dialogueText.Text = "You and your steed fell in the river.";

                                // Play a splashing sound and wait 2 seconds.
                                splashSound.Play();
                                this.Refresh();
                                Thread.Sleep(2000);
                                if (fastHorse)
                                { // If the player selected the fast horse, it's a game over, as the fast horse is not loyal and will simply run away
                                    dialogueImage.Image = Properties.Resources.SceneB9_Fall3FH;
                                    dialogueText.Text = "The horse manages to land safely and trots to a more shallow part of the river, the horse walks away while you get swept away to a waterfall.";
                                    questionLabel.Text = "Play Again?";
                                    buttonGeneration("Yes", "No", "", "");
                                    scene = "G";
                                    return;
                                }
                                else
                                { // If the player picked the loyal horse on the other hand, they both escape from the ravine-river, and head to the end of scene B9.
                                    dialogueImage.Image = Properties.Resources.SceneB9_Fall3LH;
                                    dialogueText.Text = "The horse manages to land safely on a more shallow part of the river, the horse then pulls you out of the river before you get swept away. You and your loyal steed scales the river safely.";

                                    // Wait 10 seconds to let the user read the dialogue
                                    this.Refresh();
                                    Thread.Sleep(10000);
                                    dialogueText.Text = "You make it back to town. The bandits are currently holding their weaponry at you.";
                                    dialogueImage.Image = Properties.Resources.SceneB9_Town;
                                    questionLabel.Text = "What's your move?";
                                    buttonGeneration("Dash into the bandits", "Shoot the bandits", "Weave and dodge", "");
                                    return;
                                }
                            }
                        }
                        else
                        {
                            // Trotting if normal.
                            dialogueText.Text += "Trot, ";
                        }
                    }

                    // If the user and steed make it across the bridge, update the image to show them at the town with the bandits.
                    dialogueImage.Image = Properties.Resources.SceneB9_Town;
                    if (fastHorse)
                    {
                        dialogueImage.Image = Properties.Resources.SceneB9_Town_FH;
                    }
                    dialogueText.Text = "You and your steed make it across the bridge safely, arriving in town where the bandits are holding their weaponry at you.";
                    questionLabel.Text = "What's your move?";

                    buttonGeneration("Dash into the bandits", "Shoot the bandits", "Weave and dodge", "");
                    break;
                case "B14":
                    dialogueText.Text = "You and your horse simply could not survive the barrage of bullets, and died before you could get there.";
                    dialogueImage.Image = Properties.Resources.SceneB14;
                    if (fastHorse)
                    {
                        dialogueImage.Image = Properties.Resources.SceneB14_FH;
                    }

                    // Double rifle sounds.
                    rifleSound.Play();
                    this.Refresh();
                    Thread.Sleep(300);
                    rifleSound.Play();
                    scene = "G";
                    break;
                case "B15":
                    dialogueText.Text = $"You were able to shoot 3 bandit/s before getting shot. Game over.";
                    dialogueImage.Image = Properties.Resources.SceneB15;
                    if (fastHorse)
                    {
                        dialogueImage.Image = Properties.Resources.SceneB15_FH;
                    }
                    // A for loop to signal that the user had shot three bandits and got shot themselves.
                    rifleSound.Play();
                    for (int l = 0; l < 3; l++)
                    {
                        this.Refresh();
                        Thread.Sleep(100);
                        rifleSound.Play();
                    }
                    scene = "G";
                    break;
                case "B16":
                    dialogueText.Text = "Using your experience with dodging and weaving over the years, you and your steed didn't get hit once. You're now very close to the bandits.";
                    dialogueImage.Image = Properties.Resources.SceneB16;
                    if (fastHorse)
                    {
                        dialogueImage.Image = Properties.Resources.SceneB16_FH;
                    }
                    questionLabel.Text = "Final decision?";
                    buttonGeneration("Lasso them", "Run over them", "Shoot them", "");
                    break;
                case "B17":
                    dialogueText.Text = "You managed to lasso the four bandits together, the nearby cops eventually picked them up and took them to prison. Nice work.";
                    endingB = true;
                    dialogueImage.Image = Properties.Resources.SceneB17;
                    if (fastHorse)
                    {
                        dialogueImage.Image = Properties.Resources.SceneB17_FH;
                    }
                    scene = "F";
                    break;
                case "B18":
                    dialogueText.Text = $"You were able to shoot 3 bandits before getting shot. Game over.";
                    dialogueImage.Image = Properties.Resources.SceneB18;
                    if (fastHorse)
                    {
                        dialogueImage.Image = Properties.Resources.SceneB18_FH;
                    }
                    // A for loop to signal that the user had shot three bandits and got shot themselves.
                    rifleSound.Play();
                    for (int l = 0; l < 3; l++)
                    {
                        this.Refresh();
                        Thread.Sleep(100);
                        rifleSound.Play();
                    }
                    scene = "G";
                    break;
                case "B19":
                    dialogueText.Text = "Your horse was unable to run over the bandits, and you were shot.";
                    dialogueImage.Image = Properties.Resources.SceneB19;
                    if (fastHorse)
                    {
                        dialogueImage.Image = Properties.Resources.SceneB19_FH;
                    }
                    rifleSound.Play();
                    scene = "G";
                    break;
                case "B20":
                    dialogueText.Text = "You managed to trample and squash all the bandits, I wouldn't look below you.";
                    dialogueImage.Image = Properties.Resources.SceneB20;
                    if (fastHorse)
                    {
                        dialogueImage.Image = Properties.Resources.SceneB20_FH;
                    }
                    endingB2 = true;
                    scene = "F";
                    break;
                case "C1":
                    dialogueText.Text = "You stayed put, nothing really happened. Checking the clock, it was around 3 o clock.";
                    dialogueImage.Image = Properties.Resources.SceneC1;
                    questionLabel.Text = "Leave?";
                    buttonGeneration("Yes", "No", "", "");
                    break;
                case "C2":
                    dialogueText.Text = "You seemed to be out of luck, as you were shot as you opened the door. Ending your journey quickly.";
                    dialogueImage.Image = Properties.Resources.SceneC2;
                    scene = "G";
                    break;
                case "C3":
                    dialogueText.Text = "Suddenly, the door slams open. They appeared to be bandits! The license clerk hid under their desk. While the bandits pointed their rifle at you.";
                    dialogueImage.Image = Properties.Resources.SceneC3;
                    questionLabel.Text = "What do you do?";
                    buttonGeneration("Commerce with the bandits", "Escape through their legs", "Fight back", "Hide with the clerk");
                    break;
                case "C4":
                    dialogueText.Text = "The bandits don't shoot. What will you communicate about?";
                    dialogueImage.Image = Properties.Resources.SceneC4;
                    questionLabel.Text = "What will you communicate?";
                    buttonGeneration("Make a deal with them", "Ask about the weather", "Ask why they're here", "");
                    break;
                case "C5":
                    dialogueText.Text = "What's your offer?";
                    dialogueImage.Image = Properties.Resources.SceneC5;
                    questionLabel.Text = "Decide on an offer.";
                    buttonGeneration("Your rifle", "To join them", "Gold / Money", "");
                    break;
                case "C6":
                    dialogueText.Text = "They laughed their heads off. Then they shot you.";
                    dialogueImage.Image = Properties.Resources.SceneC6;
                    rifleSound.Play();
                    scene = "G";
                    break;
                case "C7":
                    dialogueText.Text = "They come from a land far far away. Eliminating all cowboys against their bandit clan. After spitting out their knowledge, they shot you.";
                    dialogueImage.Image = Properties.Resources.SceneC7;
                    this.Refresh();
                    Thread.Sleep(500);
                    rifleSound.Play();
                    scene = "G";
                    break;
                case "C8":
                    dialogueText.Text = "The bandits already have stockpiles of rifles, they had no need for an extra one.";
                    dialogueImage.Image = Properties.Resources.SceneC8;
                    scene = "G";
                    break;
                case "C9":
                    dialogueText.Text = "The bandits did appreciate your offer, but they couldn't trust you just yet.";
                    dialogueImage.Image = Properties.Resources.SceneC9;
                    scene = "G";
                    break;
                case "C10":
                    dialogueText.Text = "The bandits liked the offer of gold. You hand them the gold and they walk out the building, sparing your life.";
                    dialogueImage.Image = Properties.Resources.SceneC10;
                    endingC = true;
                    scene = "F";
                    break;
                case "C11":
                    dialogueText.Text = "You attempted to squeeze through their legs, they just kicked your face instead.";
                    dialogueImage.Image = Properties.Resources.SceneC11;
                    scene = "G";
                    break;
                case "C12":
                    dialogueText.Text = "You sprinted to hide under the desk, but before you could get behind there, you were sniped by a bandit!";
                    dialogueImage.Image = Properties.Resources.SceneC12;
                    rifleSound.Play();
                    scene = "G";
                    break;
                case "C13":
                    dialogueText.Text = "You shot your rifle at one of the bandits.";
                    rifleSound.Play();
                    dialogueImage.Image = Properties.Resources.SceneC13_A;
                    questionLabel.Text = "";
                    buttonGeneration("", "", "", "");

                    // Wait 3 seconds
                    this.Refresh();
                    Thread.Sleep(3000);

                    // Create a value that will house the random generator, if the 30% chance was met, the player simply gets shot by a bandit
                    int shouldHitAnother = randGen.Next(1, 11);
                    if (shouldHitAnother <= 3)
                    {
                        dialogueText.Text = "You were shot by one of the bandits.";
                        dialogueImage.Image = Properties.Resources.SceneC13_B;
                        scene = "G";
                    }
                    else
                    {
                        // If successful, update the text and image
                        dialogueText.Text = "Then, you shot your rifle at another one of the bandits.";
                        dialogueImage.Image = Properties.Resources.SceneC13_C;

                        // Wait another 3 seconds
                        this.Refresh();
                        Thread.Sleep(3000);

                        // Update the random generator, if the 40% chance was met, the player gets shot by a bandit
                        shouldHitAnother = randGen.Next(1, 11);
                        if (shouldHitAnother <= 4)
                        {
                            dialogueText.Text = "You were shot by one of the bandits.";
                            dialogueImage.Image = Properties.Resources.SceneC13_D;
                            scene = "G";
                        }
                        else
                        {
                            // If successful, update the text and image
                            dialogueText.Text = "With two bandits remaining, you dodged their shots and you sniped another one. The last one aims their advanced weaponry, shooting numerous times a second.";
                            dialogueImage.Image = Properties.Resources.SceneC13_E;

                            // Wait another 3 seconds
                            this.Refresh();
                            Thread.Sleep(3000);

                            // Update the random generator, if the 50% chance was met, the player gets machine rifled by a bandit
                            shouldHitAnother = randGen.Next(1, 11);
                            if (shouldHitAnother <= 5)
                            {
                                dialogueText.Text = "You were oblierated by the last bandit's machine rifle";
                                dialogueImage.Image = Properties.Resources.SceneC13_F;
                                scene = "G";
                            }
                            else
                            {
                                // If successful, update the text and image, and update the ending variable
                                dialogueText.Text = "You managed to dodge the last one's barrel of bullets, and you shot them down. You managed to survive.";
                                dialogueImage.Image = Properties.Resources.SceneC13_G;
                                endingC2 = true;
                                scene = "F";
                            }
                        }

                    }
                    break;
                case "D1":
                    dialogueText.Text = "You enter the rifle shop.";
                    dialogueImage.Image = Properties.Resources.SceneD1;
                    questionLabel.Text = "What do you purchase?";
                    buttonGeneration("Golden Rifle", "Piercing Bullet", "Machine Rifle", "");
                    break;
                case "D2":
                    dialogueText.Text = "A great choice, if you could actually afford it! Bummer.";
                    dialogueImage.Image = Properties.Resources.SceneD2;
                    scene = "G";
                    break;
                case "D3":
                    dialogueText.Text = "You purchased the piercing bullet, you insert it into your rifle, and you go outside. The bandits are waiting for you.";
                    dialogueImage.Image = Properties.Resources.SceneD3;
                    questionLabel.Text = "";
                    buttonGeneration("", "", "", "");

                    // Wait 3 seconds
                    this.Refresh();
                    Thread.Sleep(3000);

                    // If the 1/20 chance is met, reward the player with a lucky ending.
                    int hitAllFour = randGen.Next(1, 21);
                    if (hitAllFour == 20)
                    {
                        dialogueText.Text = "You somehow managed to line all four bandits up, and pierced through all their hearts!";
                        dialogueImage.Image = Properties.Resources.SceneD3_F;
                        endingD = true;
                        scene = "F";
                    }
                    else
                    { // Unfortunate
                        dialogueText.Text = "You attempted to line them up, but you completely missed.";
                        dialogueImage.Image = Properties.Resources.SceneD3_G;
                        scene = "G";
                    }
                    break;
                case "D6":
                    dialogueText.Text = "You purchase the Machine Rifle. Outside the bandits were waiting for you. You attempt to use your new weapon, but you had no idea how it works. Game over.";
                    dialogueImage.Image = Properties.Resources.SceneD6;
                    scene = "G";
                    break;
                default:
                    break;

            }
            if (scene == "G" || scene == "F") // The game over and win statement changes the label just above the buttons depending on if the player lost or won. Then also changes the buttons
            {
                questionLabel.Text = "Play Again?";
                if (scene == "F")
                {
                    questionLabel.Text = $"You've gotten {calculateEndings()}/6 endings! Would you like to play again?";
                }
                buttonGeneration("Yes", "No", "", "");
            }
        }

        private void optionButtonA_Click(object sender, EventArgs e)
        {
            // Using the same system from the demo, update the scene accordingly based on the scene it should go to based on this scene's button info from the flowchart
            if (scene == "1") // Group 1
            {
                scene = "A1";
            }
            else if (scene == "A1") // Group A
            {
                scene = "A2";
            }
            else if (scene == "A2")
            {
                scene = "A4";
            }
            else if (scene == "B1") // Group B
            {
                scene = "B2";
            }
            else if (scene == "B3")
            {
                scene = "B4";
            }
            else if (scene == "B4")
            {
                scene = "B6";
                fastHorse = false;
            }
            else if (scene == "B6" || scene == "B7")
            {
                scene = "B9";
            }
            else if (scene == "B9")
            {
                scene = "B14";
            }
            else if (scene == "B16")
            {
                scene = "B17";
            }
            else if (scene == "C1") // Group C
            {
                int safe = randGen.Next(1, 3);
                if (safe == 1)
                {
                    scene = "A1";
                }
                else
                {
                    scene = "C2";
                }
            }
            else if (scene == "C3")
            {
                scene = "C4";
            }
            else if (scene == "C4")
            {
                scene = "C5";
            }
            else if (scene == "C5")
            {
                scene = "C8";
            }
            else if (scene == "D1") // Group D
            {
                scene = "D2";
            }
            else if (scene == "G" || scene == "F") // Group Game Over/Play Again
            {
                scene = "1";
                fastHorse = false;
                cracksInTheBridge = 0;
            }
            updateGameDisplay();
        }

        private void optionButtonB_Click(object sender, EventArgs e)
        {
            // Using the same system from the demo, update the scene accordingly based on the scene it should go to based on this scene's button info from the flowchart
            if (scene == "1") // Group 1
            {
                scene = "C1";
            }
            else if (scene == "A1") // Group A
            {
                scene = "B1";
            }
            else if (scene == "A2")
            {
                scene = "A3";
            }
            else if (scene == "B1") // Group B
            {
                scene = "B3";
            }
            else if (scene == "B4")
            {
                scene = "B7";
                fastHorse = true;
            }
            else if (scene == "B9")
            {
                scene = "B15";
            }
            else if (scene == "B16")
            {
                int horseRun = 0;
                horseRun = randGen.Next(1, 11);
                if (fastHorse == true && horseRun > 3)
                {
                    scene = "B20";
                }
                else if (fastHorse == false && horseRun > 4)
                {
                    scene = "B20";
                }
                else
                {
                    scene = "B19";
                }
            }
            else if (scene == "C1") // Group C
            {
                scene = "C3";
            }
            else if (scene == "C3")
            {
                scene = "C11";
            }
            else if (scene == "C4")
            {
                scene = "C6";
            }
            else if (scene == "C5")
            {
                scene = "C9";
            }
            else if (scene == "D1") // Group D
            {
                scene = "D3";
            }
            else if (scene == "G" || scene == "F")
            {
                Application.Exit();
            }
            updateGameDisplay();
        }

        private void optionButtonC_Click(object sender, EventArgs e)
        {
            // Using the same system from the demo, update the scene accordingly based on the scene it should go to based on this scene's button info from the flowchart
            if (scene == "B1") // Group B
            {
                scene = "D1";
            }
            else if (scene == "B4")
            {
                scene = "B8";
            }
            else if (scene == "B9")
            {
                scene = "B16";
            }
            else if (scene == "B16")
            {
                scene = "B18";
            }
            else if (scene == "C3") // Group C
            {
                scene = "C13";
            }
            else if (scene == "C4")
            {
                scene = "C7";
            }
            else if (scene == "C5")
            {
                scene = "C10";
            }
            else if (scene == "D1") // Group D
            {
                scene = "D6";
            }
            updateGameDisplay();
        }

        private void optionButtonD_Click(object sender, EventArgs e)
        {
            // Using the same system from the demo, update the scene accordingly based on the scene it should go to based on this scene's button info from the flowchart
            if (scene == "C3")
            {
                scene = "C12";
            }
            updateGameDisplay();
        }
    }
}
// Congrats, you got through it all, give yourself a glass of water!
