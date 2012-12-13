using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROnline.Models.ShortMessages;

namespace PROnline.DAL
{
    public class ShortMessageTemplateSetup : IDataSetup
    {
        public void init(Models.PROnlineContext context)
        {
            ShortMessageTemplate s1 = new ShortMessageTemplate();
            s1.ShortMessageTemplateID = Guid.NewGuid();
            s1.Title = "克服嫉妒心理";
            s1.Content = "通过课程,讲座,咨询,讨论,学习小组,实验等形式,鼓励孩子积极向上，靠努力超过对手；引导孩子广泛接触社会, 结交朋友,使其在交往中建立相互理解,信任和关心的人际关系，树立远大的理想抱负。";
            s1.Creator = new Guid("a8514e9c-eabf-412e-a744-b2c6fdb3c7e5");
            s1.CreateDate = DateTime.Now;
            s1.isDelete = false;
            context.ShortMessageTemplate.Add(s1);
            context.SaveChanges();

            ShortMessageTemplate s2 = new ShortMessageTemplate();
            s2.ShortMessageTemplateID = Guid.NewGuid();
            s2.Title = "克服自卑心理";
            s2.Content = "请给予×××更多关注和鼓励，引导孩子参加一些丰富多彩的社会实践活动，完成一些力所能及并稍有难度的任务，让他在取得成功时获得成功感，并得到心理上的满足，从而树立信心；同时让孩子有不断补偿不足的机会，引导学生不要有攀比的心理趋向，并注意通过集体给与孩子大家庭的温暖。";
            s2.Creator = new Guid("a8514e9c-eabf-412e-a744-b2c6fdb3c7e5");
            s2.CreateDate = DateTime.Now;
            s2.isDelete = false;
            context.ShortMessageTemplate.Add(s2);
            context.SaveChanges();

            ShortMessageTemplate s3 = new ShortMessageTemplate();
            s3.ShortMessageTemplateID = Guid.NewGuid();
            s3.Title = "克服孤独心理";
            s3.Content = "多和孩子聊天沟通，鼓励支持孩子多做好事，每周末帮助父母做一些力所能及的家务，老师要鼓励孩子参与班级的各类集体活动，建立集体荣誉感；尽可能为孩子安排接触高山大海森林湖泊的业余活动，培养广泛的兴趣、爱好；鼓励孩子主动接近别人、关心别人，多交朋友。";
            s3.Creator = new Guid("a8514e9c-eabf-412e-a744-b2c6fdb3c7e5");
            s3.CreateDate = DateTime.Now;
            s3.isDelete = false;
            context.ShortMessageTemplate.Add(s3);
            context.SaveChanges();

            ShortMessageTemplate s4 = new ShortMessageTemplate();
            s4.ShortMessageTemplateID = Guid.NewGuid();
            s4.Title = "克服依赖心理";
            s4.Content = "鼓励孩子逐步调整平时养成的依赖习惯，提高动手能力，向独立性强的同学学习；自己能做的事一定要自己做，自己没做过的事要锻炼做；家里自己该干的事就自己干，穿衣、洗碗、打扫卫生；遇到问题要做出属于自己的选择和判断，养成独立思考问题的习惯。学校可以安排部分班级工作，养成帮助他人的意识，让孩子有机会面对问题。";
            s4.Creator = new Guid("a8514e9c-eabf-412e-a744-b2c6fdb3c7e5");
            s4.CreateDate = DateTime.Now;
            s4.isDelete = false;
            context.ShortMessageTemplate.Add(s4);
            context.SaveChanges();

            ShortMessageTemplate s5 = new ShortMessageTemplate();
            s5.ShortMessageTemplateID = Guid.NewGuid();
            s5.Title = "克服任性心理";
            s5.Content = "家长对孩子的爱要有分寸、有节制，家长与老师家庭成员对孩子的教育要取得共识，父母、祖父母、外祖父母是对孩子施加影响的长辈。他们对孩子的教育的要求、方式方法一致，力求做到口径统一；对孩子的要求，合理的给予支持和鼓励；对不合理的要求，不能迁就、顺从，听通过说明原因，使其明白为什么不能那样做，而应该这样做;家长以身作则，为孩子树立个好榜样，同时帮助孩子提高自制力。";
            s5.Creator = new Guid("a8514e9c-eabf-412e-a744-b2c6fdb3c7e5");
            s5.CreateDate = DateTime.Now;
            s5.isDelete = false;
            context.ShortMessageTemplate.Add(s5);
            context.SaveChanges();

            ShortMessageTemplate s6 = new ShortMessageTemplate();
            s6.ShortMessageTemplateID = Guid.NewGuid();
            s6.Title = "克服害羞心理";
            s6.Content = "对孩子的优点和长处不时地给予认可，挖掘孩子的特长，鼓励他多结交朋友，多参加有益的集体活动，找到兴趣爱好，同时帮助他使用一些平静、放松语言，进行自我暗示，缓和紧张情绪，减轻心理负担。";
            s6.Creator = new Guid("a8514e9c-eabf-412e-a744-b2c6fdb3c7e5");
            s6.CreateDate = DateTime.Now;
            s6.isDelete = false;
            context.ShortMessageTemplate.Add(s6);
            context.SaveChanges();

            ShortMessageTemplate s7 = new ShortMessageTemplate();
            s7.ShortMessageTemplateID = Guid.NewGuid();
            s7.Title = "克服迷茫心理";
            s7.Content = "帮助鼓励孩子通过同龄人互相倾诉，互相沟通，共同分忧，实现心理疏通；家校联手开展心理辅导，增强孩子人际交往能力，促进班集体关系的和谐融洽。";
            s7.Creator = new Guid("a8514e9c-eabf-412e-a744-b2c6fdb3c7e5");
            s7.CreateDate = DateTime.Now;
            s7.isDelete = false;
            context.ShortMessageTemplate.Add(s7);
            context.SaveChanges();

            ShortMessageTemplate s8 = new ShortMessageTemplate();
            s8.ShortMessageTemplateID = Guid.NewGuid();
            s8.Title = "克服怯懦心理";
            s8.Content = "以真挚的感情多些对孩子的关爱，充分信任孩子的能力和勇气，避免于公开场合批评，鼓励孩子表达自己的感受，家长和老师请耐心、专心倾听孩子的发言，做最好的听众；鼓励孩子参加学校、班级以及小组的各种活动，学习如何与他人交往，但要避免一些优胜劣汰的高度竞争活动；让孩子做力所能及的事情，对孩子点滴的进步进行及时恰当的肯定和表扬，家长请耐心听取孩子心声。";
            s8.Creator = new Guid("a8514e9c-eabf-412e-a744-b2c6fdb3c7e5");
            s8.CreateDate = DateTime.Now;
            s8.isDelete = false;
            context.ShortMessageTemplate.Add(s8);
            context.SaveChanges();

            ShortMessageTemplate s9 = new ShortMessageTemplate();
            s9.ShortMessageTemplateID = Guid.NewGuid();
            s9.Title = "克服猜疑心理";
            s9.Content = "鼓励孩子相信自己，相信他人和责己严，待人宽;帮助孩子懂得平时不要总想着自己，想着别人都盯着自己,而要对自己说，并没有人特别注意我，就像我不议论别人一样，别人也不会轻易议论我;让孩子学会抛开陈腐偏见,及时开诚布公。";
            s9.Creator = new Guid("a8514e9c-eabf-412e-a744-b2c6fdb3c7e5");
            s9.CreateDate = DateTime.Now;
            s9.isDelete = false;
            context.ShortMessageTemplate.Add(s9);
            context.SaveChanges();

            ShortMessageTemplate s10 = new ShortMessageTemplate();
            s10.ShortMessageTemplateID = Guid.NewGuid();
            s10.Title = "克服冷漠心理";
            s10.Content = "身教重于言传，家长做好孩子的榜样，热情待人，关心他人，富有同情心;平日里和孩子一起观看富有教育意义的书籍和电视剧，在融洽亲子关系的同时，培养孩子热情、善良的品质；让孩子多参加社会公益活动唤醒孩子内心的热情；孩子在生活中一些看似微不足道的爱心行为，家长、老师请及时给予鼓励和表扬。";
            s10.Creator = new Guid("a8514e9c-eabf-412e-a744-b2c6fdb3c7e5");
            s10.CreateDate = DateTime.Now;
            s10.isDelete = false;
            context.ShortMessageTemplate.Add(s10);
            context.SaveChanges();

            ShortMessageTemplate s11 = new ShortMessageTemplate();
            s11.ShortMessageTemplateID = Guid.NewGuid();
            s11.Title = "摆脱失望心理";
            s11.Content = "帮助孩子根据自身条件和客观情况来规划个人循序渐进的奋斗目标，学习面对困难、挫折和各种批评，学会时刻充满希望和通过努力拼搏，把握以后的每次机会。";
            s11.Creator = new Guid("a8514e9c-eabf-412e-a744-b2c6fdb3c7e5");
            s11.CreateDate = DateTime.Now;
            s11.isDelete = false;
            context.ShortMessageTemplate.Add(s11);
            context.SaveChanges();

            ShortMessageTemplate s12 = new ShortMessageTemplate();
            s12.ShortMessageTemplateID = Guid.NewGuid();
            s12.Title = "克服冲动心理";
            s12.Content = "帮助孩子在遇到强烈的情绪刺激时，强迫自己冷静下来，并快速分析事情的前因后果，然后，采取消除冲动情绪的“缓兵之计”，用理智战胜情绪上的困扰，正确评价自己，不仅看到自己的优势，也看到自己的不足；帮助孩子学会多参加户外活动，转移注意力或暗示的方法来处理问题。";
            s12.Creator = new Guid("a8514e9c-eabf-412e-a744-b2c6fdb3c7e5");
            s12.CreateDate = DateTime.Now;
            s12.isDelete = false;
            context.ShortMessageTemplate.Add(s12);
            context.SaveChanges();

            ShortMessageTemplate s13 = new ShortMessageTemplate();
            s13.ShortMessageTemplateID = Guid.NewGuid();
            s13.Title = "克服享乐心理";
            s13.Content = "和孩子全面沟通，了解孩子的兴趣爱好是什么，把学习知识和他的兴趣爱好融合起来；结合生活中的一些实际应用的例子，让孩子发现学习并不是为了应付考试，学到的知识有很多用处，可以让人们做很多事情；家长要在家少玩多学习，给孩子营造一个家庭学习氛围。";
            s13.Creator = new Guid("a8514e9c-eabf-412e-a744-b2c6fdb3c7e5");
            s13.CreateDate = DateTime.Now;
            s13.isDelete = false;
            context.ShortMessageTemplate.Add(s13);
            context.SaveChanges();

            ShortMessageTemplate s14 = new ShortMessageTemplate();
            s14.ShortMessageTemplateID = Guid.NewGuid();
            s14.Title = "克服自负心理";
            s14.Content = "在孩子做事做决定时，可以友好提醒地发表下意见和看法，提醒他全面的认识到自己的优点和缺点，不要拿自己的优点和别人的缺点相比较；帮助孩子心中有他人、处处为别人着想，尊老爱幼，善待身边的每个人，学习取他人所长补自己之短，不断地充实、完善和提高自己。";
            s14.Creator = new Guid("a8514e9c-eabf-412e-a744-b2c6fdb3c7e5");
            s14.CreateDate = DateTime.Now;
            s14.isDelete = false;
            context.ShortMessageTemplate.Add(s14);
            context.SaveChanges();

            ShortMessageTemplate s15 = new ShortMessageTemplate();
            s15.ShortMessageTemplateID = Guid.NewGuid();
            s15.Title = "克服盲从心理";
            s15.Content = "帮助孩子注意克服事事依赖他人的习惯、充分相信自己，面对困难敢说：我能行，认定目标，坚定地走自己的路；帮助孩子多结识有才华、有胆识、品质好、乐于助人的朋友，互相学习，互相沟通，互相帮助，共同进步；遇事多想几个为什么、对不对、做事要有主见、有原则。";
            s15.Creator = new Guid("a8514e9c-eabf-412e-a744-b2c6fdb3c7e5");
            s15.CreateDate = DateTime.Now;
            s15.isDelete = false;
            context.ShortMessageTemplate.Add(s15);
            context.SaveChanges();

            ShortMessageTemplate s16 = new ShortMessageTemplate();
            s16.ShortMessageTemplateID = Guid.NewGuid();
            s16.Title = "克服脆弱心理";
            s16.Content = "为孩子创造更多的自由发展空间，极力营造一种和谐愉悦的成长氛围，巧妙地激发孩子积极表现自我的机会，使其个性得到充分展示，并积极采纳其提出的建设性建议，付诸于管理，让孩子看到成效，感受到成功的喜悦，从而增强自尊心、自信心、激发内部潜力，坚定“我能行”信念；通过故事会、报告会、学习名人、伟人对待挫折的态度，来提高孩子对挫折的认识，敢于正视挫折，同时结合“磨难教育”，让其在一次次与自己过不去的过程中，体验到人生的乐趣与辉煌，增强抗挫信心。";
            s16.Creator = new Guid("a8514e9c-eabf-412e-a744-b2c6fdb3c7e5");
            s16.CreateDate = DateTime.Now;
            s16.isDelete = false;
            context.ShortMessageTemplate.Add(s16);
            context.SaveChanges();

            ShortMessageTemplate s17 = new ShortMessageTemplate();
            s17.ShortMessageTemplateID = Guid.NewGuid();
            s17.Title = "克服浮躁心理";
            s17.Content = "帮助孩子当心情不好或为学习而烦躁时，放一曲优美、舒缓的音乐，来减轻心理上的负担，等心情平静下来了，全身地投入学习；帮助孩子学习不要跟着感觉走，清楚自己的优势和不足，目标切合实际，在实践过程中坚定意志和信念，走自己的路。";
            s17.Creator = new Guid("a8514e9c-eabf-412e-a744-b2c6fdb3c7e5");
            s17.CreateDate = DateTime.Now;
            s17.isDelete = false;
            context.ShortMessageTemplate.Add(s17);
            context.SaveChanges();
        }
    }
}