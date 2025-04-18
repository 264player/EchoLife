﻿namespace EchoLife.Will.Data
{
    /// <summary>
    /// Enum representing different types of wills as per the provisions under the Civil Code of the People's Republic of China.
    /// For more details on the legal framework governing wills, see the official NPC page on the inheritance laws:
    /// <a href="http://www.npc.gov.cn/npc/c2/c30834/202006/t20200602_306457.html">NPC Official Page on Inheritance Laws 第六编第三章</a>.
    /// The "NORMAL" type refers to the general category of wills recognized under the Civil Code, including handwritten wills, oral wills, and notarized wills.
    /// </summary>
    public enum WillType
    {
        /// <summary>
        /// A handwritten will personally written and signed by the testator.
        /// </summary>
        SELF_WRITTEN_WILL,

        /// <summary>
        /// A will written by another person under the direction of the testator, with at least two witnesses.
        /// </summary>
        WRITTEN_BY_OTHERS_WILL,

        /// <summary>
        /// A will recorded in audio format, with at least two witnesses.
        /// </summary>
        AUDIO_WILL,

        /// <summary>
        /// A will recorded in video format, with at least two witnesses.
        /// </summary>
        VIDEO_WILL,

        /// <summary>
        /// A living will that specifies medical or end-of-life decisions.
        /// </summary>
        LIVING_WILL,

        /// <summary>
        /// A will notarized by a public notary, carrying the highest legal authority.
        /// </summary>
        NOTARIZED_WILL,

        /// <summary>
        /// A will that establishes a trust to manage assets after death.
        /// </summary>
        TRUST_WILL,
    }
}
