﻿using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Collections.Generic;
// chouf lien hedha ta3 l Bot mta3i https://github.com/saibimajdi/BotNewVersion/blob/master/Dialogs/RootDialog.cs
namespace FirstBot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);//wait fothe first message
            
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            await context.PostAsync("hi i  zelootia bot i m here to help you to take care for yoir pet");
            var activity = await result as Activity;
            await context.PostAsync("do you want to know more about your pet ? ( yes / No)");
            context.Wait(MessageReceivedOptionChoice);

        }

        private async Task MessageReceivedOptionChoice(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;
            if (message.Text.ToLower().Equals("yes", StringComparison.InvariantCultureIgnoreCase))
            {
                await context.PostAsync("What's your pet name ??");
                context.Wait(MessagePetName);
            }
            else
            if (message.Text.ToLower().Equals("no", StringComparison.InvariantCultureIgnoreCase))
            {
                await context.PostAsync("Thank you ! see you later");
            }
            else
                await this.MessageReceivedAsync(context,null);
        }

// this method is replaced by the other method "MessagePetName2" 
        private async Task MessagePetName2(IDialogContext context, IAwaitable<IMessageActivity> result)
        { 
            var message = await result as Activity;
            await context.PostAsync(message.Text + " is awsome name !!");
             var messagetosend = message.CreateReply();
            messagetosend.Attachments = new List<Attachment>();
            

            if (result != null || message.Text != "")
            {
                //HeroCard herocard = new HeroCard()
                //{
                //    Title = "Cat",
                //};
                List<CardImage> images = new List<CardImage>();
                CardImage cat = new CardImage("https://images.pexels.com/photos/126407/pexels-photo-126407.jpeg");
                images.Add(cat);
                CardAction ca = new CardAction()
                {
                    Title = "Cat",
                    //Image= "https://images.pexels.com/photos/126407/pexels-photo-126407.jpeg",
                    Value = "http://saibimajdi.com/"
                    Type = "openUrl"
                };

                ThumbnailCard herocard = new ThumbnailCard()
                {
                    Title = "Is your pet a cat",
                    Tap =ca,
                    Images=images
                    
                };
                
                //List<CardImage> images = new List<CardImage>();
                //CardImage cat = new CardImage("https://images.pexels.com/photos/126407/pexels-photo-126407.jpeg");
                //images.Add(cat);
                //herocard.Images = images;
                messagetosend.Attachments.Add(herocard.ToAttachment());
                //CardImage dog = new CardImage("http://cdn2-www.dogtime.com/assets/uploads/2011/03/cute-dog-names.jpg");
                //images.Add(dog);
                //CardImage bird = new CardImage("https://s-media-cache-ak0.pinimg.com/originals/e4/bd/06/e4bd0626841d5472bb16941041562e7c.jpg");
                //images.Add(bird);

                //messagetosend.Attachments.Add(new Attachment()
                //{
                //    ContentUrl = "https://images.pexels.com/photos/126407/pexels-photo-126407.jpeg",
                //    Name = "Cat",
                //    ContentType="image/jpeg"
                //});
            }
            await context.PostAsync(messagetosend);
        }
        
        private async Task MessagePetName(IDialogContext context, IAwaitable<string> result)
        {
            var choice = await result;

                if(choice != null)
                {
                    var reply = context.MakeMessage();
                    reply.Attachments = new List<Attachment>();

                    List<CardImage> cardImages = new List<CardImage>() { new CardImage("http://saibimajdi.com/images/shared.png") };
                    List<CardAction> cardActions = new List<CardAction>()
                    {
                        new CardAction()
                        {
                            Value = "http://saibimajdi.com",
                            Title = "Majdi SAIBI Website",
                            Type = "openUrl"
                        }
                    };

                    ThumbnailCard thumbnailCard = new ThumbnailCard()
                    {
                        Title = "Hi, I'm Majdi",
                        Subtitle = "Thinker & .NET Developer",
                        Images = cardImages,
                        Buttons = cardActions,
                    };

                    reply.Attachments.Add(thumbnailCard.ToAttachment());

                    await context.PostAsync(reply);
                }
                else
                {
                    PromptDialog.Text(context, aboutDeveloperResumeAfter, "Options :", "Sorry, I can't understand you!");
                }
            }
        
        /*private async Task MessagePetspecy(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;
            if (message.Text.ToLower().Equals("1", StringComparison.InvariantCultureIgnoreCase))
            {
                await context.PostAsync("Cat is a nice choice");
                context.Wait(MessagePetName);
            }
            else
             if (message.Text.ToLower().Equals("2", StringComparison.InvariantCultureIgnoreCase))
            {
                await context.PostAsync("Dog is a nice choice");
            }
            else
                await this.MessageReceivedAsync(context, null);
        }*/
        private async Task MessagePetspecy(IDialogContext context, IAwaitable<string> result)
        {
            var message = await result as string;
            if (message.ToLower().Equals("1", StringComparison.InvariantCultureIgnoreCase))
            {
                await context.PostAsync("Cat is a nice choice");
                context.Wait(MessagePetName);
            }
            else
             if (message.ToLower().Equals("2", StringComparison.InvariantCultureIgnoreCase))
            {
                await context.PostAsync("Dog is a nice choice");
            }
            else
                await this.MessageReceivedAsync(context, null);
        }
    }
}
