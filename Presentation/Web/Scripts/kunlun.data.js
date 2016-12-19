(function () {
    var privateFunction = {
        handleUndefined: function (obj) {
            return obj === undefined ? null : obj;
        }
    }

    var selector = {
        Item: function (code, name, categoryCode, categoryName, data) {
            this.code = privateFunction.handleUndefined(code);
            this.name = privateFunction.handleUndefined(name);
            this.categoryCode = privateFunction.handleUndefined(categoryCode);
            this.categoryName = privateFunction.handleUndefined(categoryName);
            this.data = privateFunction.handleUndefined(data);

            if (this.code != null) {
                this.code = this.code.toString();
            }
        },

        Query: function (queryString, categoryCode) {
            this.queryString = privateFunction.handleUndefined(queryString);
            this.categoryCode = privateFunction.handleUndefined(categoryCode);
        }
    }

    var enums = {
        reportTypeOfDate: {
            month: "1",
            year: "4",
            day: "3",
            custom: "5"
        },
        selectBudgetType: {
            roomRate: "0",
            rentalRate: "1"
        },
        oKorNO: {
            oK: "1",
            nO: "2"
        },
        LabelQueryOfDate: {
            A: "0",
            Y: "1",
            N: "2",
            L: "3"
        },
        reportTypeOfOrderSource: {
            roomType: "1",
            rate: "2",
            market: "3",
            rateRange: "4"
        },
        guestType:{
            accountNames: "0",
            guestName:"1"
        },
        reportTypeOfGroup:{            
            customReport:"0",
            monthlyReport:"1",
            annualReport:"2"
        }
}

    window.kunlun = window.kunlun || {};
jQuery.extend(window.kunlun, {
    data: {
        Selector: selector,
        enums: enums
    }
});
})();