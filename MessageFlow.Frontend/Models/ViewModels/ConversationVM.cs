﻿using MessageFlow.Frontend.Models.DTOs;

namespace MessageFlow.Frontend.Models.ViewModels
{
    public class ConversationVM
    {
        public ConversationDTO Conversation { get; set; }
        public bool IsActiveTab { get; set; } = false;

        public ConversationVM(ConversationDTO conversation)
        {
            Conversation = conversation;
        }

        public string GetSourceIcon()
        {
            return Conversation.Source switch
            {
                "Facebook" => "images/facebook.svg",
                "WhatsApp" => "images/whatsapp.svg",
                "Gateway" => "images/sms.svg",
                _ => "icons/red-dot.svg"
            };
        }

        public string GetSourceAltText()
        {
            return Conversation.Source switch
            {
                "Facebook" => "Facebook",
                "WhatsApp" => "WhatsApp",
                "Gateway" => "Gateway",
                _ => "Unknown"
            };
        }
    }
}