(function () {

    //出租率变，则相应变化的有：
    //1。该segment该月的间夜、收入
    //2。横向：该segment的合计4项均变
    //3。竖向：该月的总计4项均变
    //4。总合计：4项均变
    //参数：i:segment   mm:月   obj:当前所变出租率控件
    var cal_per = {
        fn_cal_per: function fn_cal_per(i, mm, obj, tcnt, year) {
            var obj_nts, obj_rev, obj_avg;
            var obj_nts_sum, obj_per_sum, obj_avg_sum, obj_rev_sum;
            var obj_nts_ttl, obj_per_ttl, obj_avg_ttl, obj_rev_ttl;
            var obj_nts_ttl_sum, obj_per_ttl_sum, obj_avg_ttl_sum, obj_rev_ttl_sum;
            var dayCount = kendo.date.lastDayOfMonth(new Date(year, 1, 1)).getDate() + 337;
            var kznts = dayCount * $("#HiddenRoomNum").val();//一年可租房间数
            //得到某月可出租间天数
            var m_rooms;
            var total_rooms = $("#HiddenRoomNum").val();
            var days = kendo.date.lastDayOfMonth(new Date(year, mm, 1)).getDate();
            m_rooms = total_rooms * days;

            //不填，则显示为0
            var v_obj = obj.val();
            if (v_obj == "" || isNaN(v_obj)) {
                v_obj = 0;
                obj.val(0);
            }
            //>100,则显示为100
            if (parseInt(v_obj) > 100) {
                v_obj = 100;
                obj.val(100);
            }
            var total_per = 0.00;
            var j;
            var per_obj;
            for (j = 0; j < tcnt; j++) {
                if (j == i) {
                    total_per = total_per + parseFloat(parseInt(v_obj) / 100);
                }
                else {
                    per_obj = $("#BudgetList_" + (mm + 12 * j) + "__RentRate").getKendoNumericTextBoxWithoutSpinner();
                    total_per = total_per + parseFloat(per_obj.value() / 100);
                }
            }
            if (total_per > 1)//判断当月出租率综合是否超过100%，若超过，则修正出租率改为（100%-已设定的出租率）
            {
                var newObj = 100 - (total_per * 100 - parseFloat(obj.val()));
                if (newObj < 0)
                { newObj = 0; }
                obj.val(newObj);
                v_obj = obj.val();
            }
            //-------------------------得到各控件对象
            //该source该月的间夜、收入、房价
            count = mm + 12 * i;
            obj_nts = $("#BudgetList_" + count + "__RoomNight").getKendoNumericTextBoxWithoutSpinner();
            obj_avg = $("#BudgetList_" + count + "__AvgPrice").getKendoNumericTextBoxWithoutSpinner();//房价
            obj_rev = $("#BudgetList_" + count + "__Revenue").getKendoNumericTextBoxWithoutSpinner();//收入

            //该source12个月合计4项
            obj_nts_sum = $("#BudgetSumList_" + i + "__RoomNight").getKendoNumericTextBoxWithoutSpinner();//出租间数
            obj_avg_sum = $("#BudgetSumList_" + i + "__AvgPrice").getKendoNumericTextBoxWithoutSpinner();//房价
            obj_rev_sum = $("#BudgetSumList_" + i + "__Revenue").getKendoNumericTextBoxWithoutSpinner();//收入
            obj_per_sum = $("#BudgetSumList_" + i + "__RentRate").getKendoNumericTextBoxWithoutSpinner();//出租率

            //该月总计4项
            obj_nts_ttl = $("#BudgetTotalList_" + mm + "__RoomNight").getKendoNumericTextBoxWithoutSpinner();//出租间数
            obj_avg_ttl = $("#BudgetTotalList_" + mm + "__AvgPrice").getKendoNumericTextBoxWithoutSpinner();//房价
            obj_rev_ttl = $("#BudgetTotalList_" + mm + "__Revenue").getKendoNumericTextBoxWithoutSpinner();//收入
            obj_per_ttl = $("#BudgetTotalList_" + mm + "__RentRate").getKendoNumericTextBoxWithoutSpinner();//出租率

            //总合计4项
            obj_nts_ttl_sum = $("#RoomNightTotalSum").getKendoNumericTextBoxWithoutSpinner();//出租间数
            obj_avg_ttl_sum = $("#AvgPriceTotalSum").getKendoNumericTextBoxWithoutSpinner();//房价
            obj_rev_ttl_sum = $("#RevenueTotalSum").getKendoNumericTextBoxWithoutSpinner();//收入
            obj_per_ttl_sum = $("#RentRateTotalSum").getKendoNumericTextBoxWithoutSpinner();//出租率

            //-------------------------变值
            //该source该月的间夜、收入、房价
            var nts_old, rev_old;
            var nts_new, rev_new;
            nts_old = parseInt(obj_nts.value());
            rev_old = parseFloat(obj_rev.value());
            nts_new = Math.round(m_rooms * v_obj / 100);
            rev_new = parseFloat(obj_avg.value()) * nts_new;

            obj_nts.value(nts_new);
            obj_rev.value(JStrToMoney(Math.round(rev_new * 100) / 100));

            //该source12个月合计4项
            var nts_sum_old, rev_sum_old;
            var nts_sum_new, rev_sum_new;
            nts_sum_old = parseInt(obj_nts_sum.value());
            rev_sum_old = parseFloat(obj_rev_sum.value());
            nts_sum_new = nts_sum_old - nts_old + nts_new;
            rev_sum_new = JStrToMoney(rev_sum_old - rev_old + rev_new);

            obj_nts_sum.value(nts_sum_new);
            if (kznts == 0) {
                obj_per_sum.value(0.00);
            }
            else {
                obj_per_sum.value(JStrToMoney(Math.round(nts_sum_new * 10000 / kznts) / 100));
            }
            if (nts_sum_new == 0) {
                obj_avg_sum.value(0.00);
            }
            else {
                obj_avg_sum.value(JStrToMoney(Math.round(rev_sum_new * 100 / nts_sum_new) / 100));
            }
            if (isNaN(obj_avg_sum.value())) {
                obj_avg_sum.value(0.00);
            }
            obj_rev_sum.value(JStrToMoney(Math.round(rev_sum_new * 100) / 100));

            //该月总计4项
            var nts_ttl_old, rev_ttl_old;
            var nts_ttl_new, rev_ttl_new;
            if (obj_nts_ttl.value() == "") {
                obj_nts_ttl.value(0);
            }
            if (obj_rev_ttl.value() == "") {
                obj_rev_ttl.value(0.00);
            }
            nts_ttl_old = parseInt(obj_nts_ttl.value());
            rev_ttl_old = parseFloat(obj_rev_ttl.value());
            nts_ttl_new = nts_ttl_old - nts_old + nts_new;
            rev_ttl_new = rev_ttl_old - rev_old + rev_new;

            obj_nts_ttl.value(nts_ttl_new);
            if (m_rooms == 0) {
                obj_per_ttl.value(0.00);
            }
            else {
                obj_per_ttl.value(Math.round(nts_ttl_new * 10000 / m_rooms) / 100);
            }

            if (isNaN(obj_per_ttl.value())) {
                obj_per_ttl.value(0.00);
            }
            if (nts_ttl_new == 0) {
                obj_avg_ttl.value(0.00);
            }
            else {
                obj_avg_ttl.value(JStrToMoney(Math.round(rev_ttl_new * 100 / nts_ttl_new) / 100));
            }
            if (isNaN(obj_avg_ttl.value())) {
                obj_avg_ttl.value(0.00);
            }
            obj_rev_ttl.value(JStrToMoney(Math.round(rev_ttl_new * 100) / 100));

            //总合计4项
            var nts_ttl_sum_old, rev_ttl_sum_old;
            var nts_ttl_sum_new, rev_ttl_sum_new;
            nts_ttl_sum_old = parseInt(obj_nts_ttl_sum.value());
            rev_ttl_sum_old = parseFloat(obj_rev_ttl_sum.value());
            nts_ttl_sum_new = nts_ttl_sum_old - nts_old + nts_new;
            rev_ttl_sum_new = rev_ttl_sum_old - rev_old + rev_new;

            obj_nts_ttl_sum.value(nts_ttl_sum_new);
            if (kznts == 0) {
                obj_per_ttl_sum.value(0.00);
            }
            else {
                obj_per_ttl_sum.value(JStrToMoney(Math.round(nts_ttl_sum_new * 10000 / kznts) / 100));
            }
            if (nts_ttl_sum_new == 0) {
                obj_avg_ttl_sum.value(0.00);
            }
            else {
                obj_avg_ttl_sum.value(JStrToMoney(Math.round(rev_ttl_sum_new * 100 / nts_ttl_sum_new) / 100));
            }
            obj_rev_ttl_sum.value(Math.round(rev_ttl_sum_new * 100) / 100);
            if (obj.val() == "" || parseFloat(obj.val()) == 0) {
                obj.val(0);
            } else {
                obj.val(JStrToMoney(obj.val()));
            }

        }
    }

    //出租间数变，则相应变化的有：
    //1。该segment该月的出租率、收入
    //2。横向：该segment的合计4项均变
    //3。竖向：该月的总计4项均变
    //4。总合计：4项均变
    //参数：i:segment   mm:月   obj:当前所变出租率控件   total_rooms:房间数   days:当月天数
    var cal_nts = {
        fn_cal_nts: function fn_cal_nts(i, mm, obj, tcnt, year) {
            var obj_per, obj_rev, obj_avg;
            var obj_nts_sum, obj_per_sum, obj_avg_sum, obj_rev_sum;
            var obj_nts_ttl, obj_per_ttl, obj_avg_ttl, obj_rev_ttl;
            var obj_nts_ttl_sum, obj_per_ttl_sum, obj_avg_ttl_sum, obj_rev_ttl_sum;
            var count;
            var dayCount = kendo.date.lastDayOfMonth(new Date(year, 1, 1)).getDate() + 337;
            var kznts = dayCount * $("#HiddenRoomNum").val();//一年可租房间数

            //得到某月可出租间天数
            var m_rooms;
            var total_rooms = $("#HiddenRoomNum").val();
            var days = kendo.date.lastDayOfMonth(new Date(year, mm, 1)).getDate();

            m_rooms = total_rooms * days;

            //出租间数不填，则显示为0
            var v_obj = obj.val();
            if (v_obj == "" || isNaN(v_obj)) {
                v_obj = 0;//
                obj.val(0);
            }
            //当出租间数大于当前可出租间数,则显示为出租间数
            if (parseInt(v_obj) > m_rooms) {
                v_obj = m_rooms;
                obj.val(m_rooms);
            }
            var total_nts = 0;
            var j;
            var nts_obj;
            for (j = 0; j < tcnt; j++) {
                if (j == i) {
                    total_nts = total_nts + parseInt(v_obj);
                }
                else {
                    nts_obj = $("#BudgetList_" + (mm + 12 * j) + "__RoomNight").getKendoNumericTextBoxWithoutSpinner();
                    total_nts = total_nts + parseInt(nts_obj.value());
                }
            }

            if (total_nts >= m_rooms)//判断当月出租间数是否超过当月总房数，若超过，则修正出间夜数改为（当月总房数-已出租的房数）
            {
                var newObj = m_rooms - (total_nts - parseFloat(obj.val()));
                if (newObj < 0)
                { newObj = 0; }
                obj.val(newObj);
                v_obj = obj.val();
            }
            //-------------------------得到各控件对象
            //该segment该月的出租率、收入、房价
            count = mm + 12 * i;
            obj_per = $("#BudgetList_" + count + "__RentRate").getKendoNumericTextBoxWithoutSpinner();
            obj_nts = $("#BudgetList_" + count + "__RoomNight").getKendoNumericTextBoxWithoutSpinner();
            obj_avg = $("#BudgetList_" + count + "__AvgPrice").getKendoNumericTextBoxWithoutSpinner();//房价
            obj_rev = $("#BudgetList_" + count + "__Revenue").getKendoNumericTextBoxWithoutSpinner();//收入
            //该segment12个月合计4项

            obj_nts_sum = $("#BudgetSumList_" + i + "__RoomNight").getKendoNumericTextBoxWithoutSpinner();//出租间数
            obj_avg_sum = $("#BudgetSumList_" + i + "__AvgPrice").getKendoNumericTextBoxWithoutSpinner();//房价
            obj_rev_sum = $("#BudgetSumList_" + i + "__Revenue").getKendoNumericTextBoxWithoutSpinner();//收入
            obj_per_sum = $("#BudgetSumList_" + i + "__RentRate").getKendoNumericTextBoxWithoutSpinner();//出租率

            //该月总计4项
            obj_nts_ttl = $("#BudgetTotalList_" + mm + "__RoomNight").getKendoNumericTextBoxWithoutSpinner();//出租间数
            obj_avg_ttl = $("#BudgetTotalList_" + mm + "__AvgPrice").getKendoNumericTextBoxWithoutSpinner();//房价
            obj_rev_ttl = $("#BudgetTotalList_" + mm + "__Revenue").getKendoNumericTextBoxWithoutSpinner();//收入
            obj_per_ttl = $("#BudgetTotalList_" + mm + "__RentRate").getKendoNumericTextBoxWithoutSpinner();//出租率

            //总合计4项
            obj_nts_ttl_sum = $("#RoomNightTotalSum").getKendoNumericTextBoxWithoutSpinner();//出租间数
            obj_avg_ttl_sum = $("#AvgPriceTotalSum").getKendoNumericTextBoxWithoutSpinner();//房价
            obj_rev_ttl_sum = $("#RevenueTotalSum").getKendoNumericTextBoxWithoutSpinner();//收入
            obj_per_ttl_sum = $("#RentRateTotalSum").getKendoNumericTextBoxWithoutSpinner();//出租率

            //保留两位小数

            //-------------------------变值
            //该segment该月的间夜、收入、房价
            var per_old, rev_old;
            var per_new, rev_new;
            per_old = parseFloat(obj_per.value());
            rev_old = parseFloat(obj_rev.value());
            if (m_rooms == 0) {
                per_new = 0.00;
            }
            else {
                per_new = parseFloat(v_obj * 100 / m_rooms);
            }
            rev_new = parseFloat(obj_avg.value()) * v_obj;

            obj_per.value(per_new);
            obj_rev.value(JStrToMoney(Math.round(rev_new * 100) / 100));

            //该segment12个月合计4项
            var rev_sum_old, rev_sum_new;
            var nts_sum_old, nts_sum_new;
            nts_sum_old = obj_nts_sum.value();
            nts_sum_new = parseInt(v_obj) - parseInt(obj_nts.value()) + parseInt(nts_sum_old);
            rev_sum_old = parseFloat(obj_rev_sum.value());
            rev_sum_new = JStrToMoney(rev_sum_old - rev_old + rev_new);

            obj_nts_sum.value(nts_sum_new);
            if (kznts == 0) {
                obj_per_sum.value(0.00);
            }
            else {
                obj_per_sum.value(JStrToMoney(Math.round(nts_sum_new * 10000 / kznts) / 100));
            }
            if (nts_sum_new == 0)
                obj_avg_sum.value(0.00);
            else
                obj_avg_sum.value(JStrToMoney(Math.round(rev_sum_new * 100 / nts_sum_new) / 100));

            obj_rev_sum.value(JStrToMoney(Math.round(rev_sum_new * 100) / 100));

            //该月总计4项
            var nts_ttl_old, rev_ttl_old;
            var nts_ttl_new, rev_ttl_new;
            if (obj_nts_ttl.value() == "") {
                obj_nts_ttl.value(0);
            }
            if (obj_rev_ttl.value() == "") {
                obj_rev_ttl.value(0.00);
            }
            nts_ttl_old = parseInt(obj_nts_ttl.value());
            rev_ttl_old = parseFloat(obj_rev_ttl.value());
            nts_ttl_new = parseInt(nts_ttl_old) +  parseInt(v_obj) -  parseInt(obj_nts.value());
            rev_ttl_new = rev_ttl_old - rev_old + rev_new;
            if (isNaN(nts_ttl_new)) {
                nts_ttl_new.value(0);
            }
            obj_nts_ttl.value(nts_ttl_new);
            if (m_rooms == 0) {
                obj_per_ttl.value(0.00);
            }
            else {
                obj_per_ttl.value(Math.round(nts_ttl_new * 10000 / m_rooms) / 100);
            }
            if (nts_ttl_new == 0) {
                obj_avg_ttl.value(0.00);
            }
            else {
                obj_avg_ttl.value(JStrToMoney(Math.round(rev_ttl_new * 100 / nts_ttl_new) / 100));
            }

            obj_rev_ttl.value(JStrToMoney(Math.round(rev_ttl_new * 100) / 100));

            //总合计4项
            var nts_ttl_sum_old, rev_ttl_sum_old;
            var nts_ttl_sum_new, rev_ttl_sum_new;
            nts_ttl_sum_old = parseInt(obj_nts_ttl_sum.value());
            rev_ttl_sum_old = parseFloat(obj_rev_ttl_sum.value());
            nts_ttl_sum_new = parseInt(nts_ttl_sum_old) +  parseInt(v_obj) -  parseInt(obj_nts.value());
            rev_ttl_sum_new = rev_ttl_sum_old - rev_old + rev_new;
            if (isNaN(nts_ttl_sum_new)) {
                nts_ttl_sum_new.value(0);
            }
            obj_nts_ttl_sum.value(nts_ttl_sum_new);
            if (kznts == 0) {
                obj_per_ttl_sum.value(0.00);
            }
            else {
                obj_per_ttl_sum.value(JStrToMoney(Math.round(nts_ttl_sum_new * 10000 / kznts) / 100));
            }
            if (nts_ttl_sum_new == 0) {
                obj_avg_ttl_sum.value(0.00);
            }
            else {
                obj_avg_ttl_sum.value(JStrToMoney(Math.round(rev_ttl_sum_new * 100 / nts_ttl_sum_new) / 100));
            }
            if (isNaN(obj_avg_ttl_sum.value())) {
                obj_avg_ttl_sum.value(0.00);
            }
            obj_rev_ttl_sum.value(Math.round(rev_ttl_sum_new * 100) / 100);
            if (obj.val() == "" || parseFloat(obj.val()) == 0) {
                obj.val(0);
            } else {
                obj.val(JStrToMoney(obj.val()));
            }

        }
    }

    //房价变，则相应变化的有：
    //1。该source该月的收入
    //2。横向：该source的合计收入、房价变化
    //3。竖向：该月的总计收入、房价变化
    //4。总合计：收入、房价
    //参数：i:source   mm:月   obj:当前所变出租率控件
    var cal_avg = {
        fn_cal_avg: function fn_cal_avg(i, mm, obj) {
            var obj_rev, obj_rev_sum, obj_rev_ttl, obj_rev_ttl_sum;
            var obj_avg_sum, obj_avg_ttl, obj_avg_ttl_sum;
            var obj_nts, obj_nts_sum, obj_nts_ttl, obj_nts_ttl_sum;
            var rev_sum_old, rev_sum_new;
            var rev_old, rev_new;
            var rev_ttl_old, rev_ttl_new;
            var rev_ttl_sum_old, rev_ttl_sum_new;

            //初始化变量
            rev_sum_old = rev_sum_new = rev_old = rev_new = rev_ttl_old = rev_ttl_new = rev_ttl_sum_old = rev_ttl_sum_new = 0;

            //不填，则显示为0
            var v_obj = obj.val();
            if (v_obj == "" || isNaN(v_obj)) {
                v_obj = 0;
                obj.val(0);
            }

            //-------------------------得到各控件对象
            //该source该月的收入
            count = mm + 12 * i;
            obj_nts = $("#BudgetList_" + count + "__RoomNight").getKendoNumericTextBoxWithoutSpinner();
            obj_rev = $("#BudgetList_" + count + "__Revenue").getKendoNumericTextBoxWithoutSpinner();//收入

            //该source的合计收入、房价
            obj_nts_sum = $("#BudgetSumList_" + i + "__RoomNight").getKendoNumericTextBoxWithoutSpinner();//出租间数
            obj_avg_sum = $("#BudgetSumList_" + i + "__AvgPrice").getKendoNumericTextBoxWithoutSpinner();//房价
            obj_rev_sum = $("#BudgetSumList_" + i + "__Revenue").getKendoNumericTextBoxWithoutSpinner();//收入

            //该月的总计收入、房价
            obj_nts_ttl = $("#BudgetTotalList_" + mm + "__RoomNight").getKendoNumericTextBoxWithoutSpinner();//出租间数
            obj_avg_ttl = $("#BudgetTotalList_" + mm + "__AvgPrice").getKendoNumericTextBoxWithoutSpinner();//房价
            obj_rev_ttl = $("#BudgetTotalList_" + mm + "__Revenue").getKendoNumericTextBoxWithoutSpinner();//收入

            //总合计：收入、房价
            obj_nts_ttl_sum = $("#RoomNightTotalSum").getKendoNumericTextBoxWithoutSpinner();//出租间数
            obj_avg_ttl_sum = $("#AvgPriceTotalSum").getKendoNumericTextBoxWithoutSpinner();//房价
            obj_rev_ttl_sum = $("#RevenueTotalSum").getKendoNumericTextBoxWithoutSpinner();//收入

            //-------------------------变值
            //该source该月的收入
            rev_old = parseFloat(obj_rev.value());
            rev_new = parseFloat(v_obj * obj_nts.value())
            obj_rev.value(JStrToMoney(Math.round(rev_new * 100) / 100));
            //该source的合计收入、房价
            rev_sum_old = parseFloat(obj_rev_sum.value());
            rev_sum_new = rev_sum_old - rev_old + rev_new;

            obj_rev_sum.value(JStrToMoney(Math.round(rev_sum_new * 100) / 100));
            if (obj_nts_sum.value() == 0) {
                obj_avg_sum.value(0.00);
            }
            else {
                obj_avg_sum.value(JStrToMoney(Math.round(rev_sum_new * 100 / obj_nts_sum.value()) / 100));
            }

            //该月的总计收入、房价
            rev_ttl_old = parseFloat(obj_rev_ttl.value());
            rev_ttl_new = rev_ttl_old - rev_old + rev_new;


            obj_rev_ttl.value(JStrToMoney(Math.round(rev_ttl_new * 100) / 100));
            if (obj_nts_ttl.value() == 0) {
                obj_avg_ttl.value(0.00);
            }
            else {
                obj_avg_ttl.value(JStrToMoney(Math.round(rev_ttl_new * 100 / obj_nts_ttl.value()) / 100));
            }

            //总合计：收入、房价
            rev_ttl_sum_old = parseFloat(obj_rev_ttl_sum.value());
            rev_ttl_sum_new = rev_ttl_sum_old - rev_old + rev_new;

            obj_rev_ttl_sum.value(JStrToMoney(Math.round(rev_ttl_sum_new * 100) / 100));
            if (obj_nts_ttl_sum.value() == 0)
                obj_avg_ttl_sum.value(0.00);
            else
                obj_avg_ttl_sum.value(JStrToMoney(Math.round(rev_ttl_sum_new * 100 / obj_nts_ttl_sum.value()) / 100));

            if (obj.val() == "" || obj.val() == 0) {
                obj.val(0.00);
            } else {
                obj.val(JStrToMoney(obj.val()));
            }

        }
    }

    //统一上(下)调出租率或房价，界面的变化
    var cal_all = {
        fn_cal_all: function fn_cal_all(cnt, year) {
            var v_new = $("#ModifyNum").val();//调整值
            var v_sel = $("#EnumRoomRateType").val();//调整类型
            var total_rooms = $("#HiddenRoomNum").val();//调整的房间数
            var dayCount = kendo.date.lastDayOfMonth(new Date(year, 1, 1)).getDate() + 337;
            var kznts = dayCount * $("#HiddenRoomNum").val();//一年可租房间数
            var s, i;
            var obj_nts, obj_avg, obj_per, obj_rev;
            var obj_nts_sum, obj_avg_sum, obj_per_sum, obj_rev_sum;
            var obj_nts_ttl, obj_avg_ttl, obj_per_ttl, obj_rev_ttl;
            var obj_days_dt;
            var sum_nts = sum_rev = 0;
            var ttl_sum_nts = ttl_sum_rev = 0;
            var ttl_nts = new Array();
            var ttl_rev = new Array();
            var count = 0;
            for (i = 0; i < 12; i++) {
                ttl_nts[i] = 0;
                ttl_rev[i] = 0;
            }

            //未填值或为0，则置为0且不做更新
            if (v_new == "" || v_new == 0) {
                v_new = 0;
                $("#ModifyNum").val(v_new);
                return false;
            }

            for (s = 0; s < cnt; s++)//-------------------beging循环source
            {
                sum_nts = sum_rev = 0;//清sum
                for (var i = 0; i < 12; i++)//---------beging循环月
                {
                    count = i + 12 * s;
                    //某source某月的各对象
                    obj_avg = $("#BudgetList_" + count + "__AvgPrice").getKendoNumericTextBoxWithoutSpinner();//房价
                    obj_rev = $("#BudgetList_" + count + "__Revenue").getKendoNumericTextBoxWithoutSpinner();//收入
                    obj_nts = $("#BudgetList_" + count + "__RoomNight").getKendoNumericTextBoxWithoutSpinner();//出租间数
                    obj_per = $("#BudgetList_" + count + "__RentRate").getKendoNumericTextBoxWithoutSpinner();//出租率
                    obj_days_dt = kendo.date.lastDayOfMonth(new Date(year, i, 1)).getDate();//每月天数
                    if (v_sel == "0") {

                        //---若调房价：变房价、收入、合计房价、合计收入
                        if (parseFloat(v_new) > 0) {
                            obj_avg.value(JStrToMoney(Math.round((parseFloat(obj_avg.value()) + parseFloat(v_new)) * 100) / 100));
                        }
                        else {
                            if ((parseFloat(obj_avg.value()) + parseFloat(v_new)) < 0)
                                obj_avg.value(0.00);
                            else
                                obj_avg.value(JStrToMoney(Math.round((parseFloat(obj_avg.value()) + parseFloat(v_new)) * 100) / 100));
                        }
                        obj_rev.value(JStrToMoney(Math.round(parseFloat(obj_avg.value()) * parseInt(obj_nts.value()) * 100) / 100));

                    }
                    else {
                        //---若调出租率：变出租率、间夜、收入、合计间夜、合计收入、合计房价、合计出租率
                        if (v_new > 100) {
                            v_new = 100;
                            $("#ModifyNum").val(v_new);
                        }

                        if (parseFloat(v_new) > 0) {
                            if ((parseFloat(obj_per.value()) + parseFloat(v_new)) > 100)
                                obj_per.value(100);
                            else
                                obj_per.value(JStrToMoney(Math.round((parseFloat(obj_per.value()) + parseFloat(v_new)) * 100) / 100));
                        }
                        else {
                            if ((parseFloat(obj_per.value()) + parseFloat(v_new)) < 0)
                                obj_per.value(0);
                            else
                                obj_per.value(JStrToMoney(Math.round((parseFloat(obj_per.value()) + parseFloat(v_new)) * 100) / 100));
                        }
                        obj_nts.value(Math.round(parseFloat(obj_per.value()) / 100 * total_rooms * obj_days_dt));
                        obj_rev.value(JStrToMoney(Math.round(parseFloat(obj_avg.value()) * parseInt(obj_nts.value()) * 100) / 100));
                    }

                    //累加得该source的合计
                    sum_nts = sum_nts + parseInt(obj_nts.value());
                    sum_rev = sum_rev + parseFloat(obj_rev.value());

                    //累加得各月的总计
                    ttl_nts[i] = ttl_nts[i] + parseInt(obj_nts.value());
                    ttl_rev[i] = ttl_rev[i] + parseFloat(obj_rev.value());

                }//---------end循环月
                //某source的12个月的合计对象
                obj_nts_sum = $("#BudgetSumList_" + s + "__RoomNight").getKendoNumericTextBoxWithoutSpinner();//出租间数
                obj_avg_sum = $("#BudgetSumList_" + s + "__AvgPrice").getKendoNumericTextBoxWithoutSpinner();//房价
                obj_rev_sum = $("#BudgetSumList_" + s + "__Revenue").getKendoNumericTextBoxWithoutSpinner();//收入
                obj_per_sum = $("#BudgetSumList_" + s + "__RentRate").getKendoNumericTextBoxWithoutSpinner();//出租率

                obj_nts_sum.value(parseFloat(sum_nts));
                obj_rev_sum.value(JStrToMoney(Math.round(sum_rev * 100) / 100));
                if (sum_nts == 0) {
                    obj_avg_sum.value(0.00);
                }
                else {
                    obj_avg_sum.value(JStrToMoney(Math.round(sum_rev * 100 / sum_nts) / 100));
                }
                if (kznts == 0) {
                    obj_per_sum.value(0.00);
                }
                else {
                    obj_per_sum.value(JStrToMoney(Math.round(sum_nts * 10000 / kznts) / 100));
                }

            }//-------------------end循环source


            //各source的每个月的总计对象
            for (i = 0; i < 12; i++) {
                obj_nts_ttl = $("#BudgetTotalList_" + i + "__RoomNight").getKendoNumericTextBoxWithoutSpinner();//出租间数
                obj_avg_ttl = $("#BudgetTotalList_" + i + "__AvgPrice").getKendoNumericTextBoxWithoutSpinner();//房价
                obj_rev_ttl = $("#BudgetTotalList_" + i + "__Revenue").getKendoNumericTextBoxWithoutSpinner();//收入
                obj_per_ttl = $("#BudgetTotalList_" + i + "__RentRate").getKendoNumericTextBoxWithoutSpinner();//出租率
                obj_days_dt = kendo.date.lastDayOfMonth(new Date(year, i, 1)).getDate();//每月天数

                obj_nts_ttl.value(ttl_nts[i]);
                obj_rev_ttl.value(JStrToMoney(Math.round(ttl_rev[i] * 100) / 100));
                if (ttl_nts[i] == 0)
                    obj_avg_ttl.value(0.00);
                else
                    obj_avg_ttl.value(JStrToMoney(Math.round(ttl_rev[i] * 100 / ttl_nts[i]) / 100))
                if (total_rooms * obj_days_dt == 0) {
                    obj_per_ttl.value(0.00);
                }
                else {
                    obj_per_ttl.value(ttl_nts[i] * 100 / (total_rooms * obj_days_dt));
                }
                ttl_sum_nts = ttl_sum_nts + ttl_nts[i];
                ttl_sum_rev = ttl_sum_rev + ttl_rev[i];
            }

            //总合计
            obj_nts_ttl_sum = $("#RoomNightTotalSum").getKendoNumericTextBoxWithoutSpinner();//出租间数
            obj_avg_ttl_sum = $("#AvgPriceTotalSum").getKendoNumericTextBoxWithoutSpinner();//房价
            obj_rev_ttl_sum = $("#RevenueTotalSum").getKendoNumericTextBoxWithoutSpinner();//收入
            obj_per_ttl_sum = $("#RentRateTotalSum").getKendoNumericTextBoxWithoutSpinner();//出租率
            obj_rev_ttl_sum.value(JStrToMoney(Math.round(ttl_sum_rev * 100) / 100));
            obj_nts_ttl_sum.value(ttl_sum_nts);
            if (ttl_sum_nts == 0) {
                obj_avg_ttl_sum.value(0.00);
            }
            else {
                obj_avg_ttl_sum.value(JStrToMoney(Math.round(ttl_sum_rev * 100 / ttl_sum_nts) / 100));
            }
            if (kznts == 0) {
                obj_per_ttl_sum.value(0.00);
            }
            else {
                obj_per_ttl_sum.value(JStrToMoney(Math.round(ttl_sum_nts * 10000 / kznts) / 100));
            }
        }
    }


    //更改房间数，则变化的有：
    //1.根据现有的房间出租率更改出租间数、可出租间数、收入
    //2.横向：所有的合计收入、出租间数变化
    //3.竖向：该月的总计收入、出租间数变化
    //4.总合计：收入、出租间数
    var change_HiddenRoomNum = {
        js_change: function js_change(scnt, year) {
            var i, j;

            var obj_nts, obj_avg, obj_per, obj_rev;
            var obj_nts_sum, obj_avg_sum, obj_per_sum, obj_rev_sum;
            var obj_mon;
            var obj_nts_ttl, obj_avg_ttl, obj_per_ttl, obj_rev_ttl;
            var sum_nts = sum_rev = 0;
            var ttl_sum_nts = ttl_sum_rev = 0;
            var ttl_nts = new Array();
            var ttl_rev = new Array();
            var sum_total_rooms;
            var avg_tmp;
            var total_rooms = $("#HiddenRoomNum").val();;
            var monthRoom = 0;

            //重新设定每月可租房间数
            for (i = 0; i < 12; i++)//
            {
                monthRoom = total_rooms * kendo.date.lastDayOfMonth(new Date(year, i, 1)).getDate();
                $("#SaleRoomList\\[" + i + "\\]").text(monthRoom)
                for (j = 0; j < scnt; j++) {
                    $("#BudgetList_" + (i + j * 12) + "__HotelRoomNum").val(monthRoom);
                }
                ttl_nts[i] = 0;
                ttl_rev[i] = 0;
            }
            //重新计算合计可租房数
            sum_total_rooms = total_rooms * (kendo.date.lastDayOfMonth(new Date(year, 1, 1)).getDate() + 337);
            $("#SaleRoomList").text(sum_total_rooms);

            for (j = 0; j < scnt; j++) {

                sum_nts = 0;
                sum_rev = 0;
                for (i = 0; i < 12; i++) {
                    count = i + 12 * j;
                    $("#BudgetList_" + count + "__HotelHiddenRoomNum").val(total_rooms * kendo.date.lastDayOfMonth(new Date(year, i, 1)).getDate());
                    obj_avg = $("#BudgetList_" + count + "__AvgPrice").getKendoNumericTextBoxWithoutSpinner();//房价
                    obj_rev = $("#BudgetList_" + count + "__Revenue").getKendoNumericTextBoxWithoutSpinner();//收入
                    obj_nts = $("#BudgetList_" + count + "__RoomNight").getKendoNumericTextBoxWithoutSpinner();//出租间数
                    obj_per = $("#BudgetList_" + count + "__RentRate").getKendoNumericTextBoxWithoutSpinner();//出租率
                    obj_mon = kendo.date.lastDayOfMonth(new Date(year, i, 1)).getDate() * total_rooms;//每月可出租房

                    //重新计算出租房间数
                    obj_nts.value(Math.round(parseInt(obj_mon) * parseFloat(obj_per.value()) / 100));//月可出租房间数*出租率

                    obj_rev.value(parseFloat(obj_avg.value()) * parseInt(obj_nts.value()));  //房价*出租房数


                    //累加得该segment的合计
                    sum_nts = sum_nts + parseInt(obj_nts.value());
                    sum_rev = sum_rev + parseFloat(obj_rev.value());

                    //累加得各月的总计
                    ttl_nts[i] = ttl_nts[i] + parseInt(obj_nts.value());
                    ttl_rev[i] = ttl_rev[i] + parseFloat(obj_rev.value());

                }
                //计算该市场合计出租房间数以及收入
                $("#BudgetSumList_" + j + "__RoomNight").getKendoNumericTextBoxWithoutSpinner().value(sum_nts);
                $("#BudgetSumList_" + j + "__Revenue").getKendoNumericTextBoxWithoutSpinner().value(JStrToMoney(sum_rev));
                //计算出租率及平均房价
                $("#BudgetSumList_" + j + "__RentRate").getKendoNumericTextBoxWithoutSpinner().value(JStrToMoney(parseFloat(sum_nts / sum_total_rooms) * 100));
                avg_tmp = parseFloat(sum_rev / sum_nts);
                if (isNaN(avg_tmp)) {
                    avg_tmp = 0.00;
                }
                $("#BudgetSumList_" + j + "__AvgPrice").getKendoNumericTextBoxWithoutSpinner().value(JStrToMoney(avg_tmp));

            }
            //计算总计每月总计
            for (i = 0; i < 12; i++)//
            {
                $("#BudgetTotalList_" + i + "__RoomNight").getKendoNumericTextBoxWithoutSpinner().value(ttl_nts[i]);

                $("#BudgetTotalList_" + i + "__Revenue").getKendoNumericTextBoxWithoutSpinner().value(JStrToMoney(ttl_rev[i]));
                $("#BudgetTotalList_" + i + "__RentRate").getKendoNumericTextBoxWithoutSpinner().value(parseFloat(ttl_nts[i] / (total_rooms * kendo.date.lastDayOfMonth(new Date(year, i, 1)).getDate())) * 100);
                avg_tmp = parseFloat(ttl_rev[i] / ttl_nts[i]);
                if (isNaN(avg_tmp)) {
                    avg_tmp = 0.00;
                }
                $("#BudgetTotalList_" + i + "__AvgPrice").getKendoNumericTextBoxWithoutSpinner().value(JStrToMoney(avg_tmp));
                ttl_sum_nts = ttl_sum_nts + ttl_nts[i];
                ttl_sum_rev = ttl_sum_rev + ttl_rev[i];

            }
            //计算该总计合计出租房间数以及收入
            $("#RoomNightTotalSum").getKendoNumericTextBoxWithoutSpinner().value(JStrToMoney(ttl_sum_nts));
            $("#RevenueTotalSum").getKendoNumericTextBoxWithoutSpinner().value(JStrToMoney(ttl_sum_rev));
            //计算出租率及平均房价
            $("#RentRateTotalSum").getKendoNumericTextBoxWithoutSpinner().value(JStrToMoney(parseFloat(ttl_sum_nts / sum_total_rooms) * 100));
            avg_tmp = JStrToMoney(parseFloat(ttl_sum_rev / ttl_sum_nts));
            if (isNaN(avg_tmp)) {
                avg_tmp = 0.00;
            }
            $("#AvgPriceTotalSum").getKendoNumericTextBoxWithoutSpinner().value(JStrToMoney(avg_tmp));


        }
    }



    function formatFloat(value) {
        value = Math.round(parseFloat(value) * 100) / 100;
        if (value.toString().indexOf(".") < 0)
            value = value.toString() + ".00";
        return value;
    }

    function JStrToMoney(StrMoney) {
        var tmpstr
        tmpstr = StrMoney.toString();

        if (tmpstr.indexOf(".") == -1)
            return tmpstr + ".00";
        else   //存在小数点的情况，小数点后的位数大于2则多余部分处理掉，小于2则用0补足
        {
            if (tmpstr.length - tmpstr.indexOf(".") > 3)
                return tmpstr.substr(0, tmpstr.indexOf(".") + 3);
            else if (tmpstr.length - tmpstr.indexOf(".") == 2)
                return tmpstr + "0";
            else if (tmpstr.length - tmpstr.indexOf(".") == 1)
                return tmpstr + "00";
            else
                return tmpstr;
        }
    }

    jQuery.extend({
        calPer: cal_per,
        calNts: cal_nts,
        calAvg: cal_avg,
        calAll: cal_all,
        changeHiddenRoomNum: change_HiddenRoomNum
    });
})();


