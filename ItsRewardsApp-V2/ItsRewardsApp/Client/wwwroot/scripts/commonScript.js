////$(document)
////    .on("swipeleft", ".item-swipe", function (e) {
////        $(this).nextAll("div").addClass("show");
////        $(this).off("click").blur();
////        $(this)
////            .css({
////                transform: "translateX(-130px)",
////            })
////            .one("transitionend webkitTransitionEnd oTransitionEnd otransitionend MSTransitionEnd", function () {
////                $(this).one("swiperight", function () {
////                    $(this).nextAll("div").removeClass("show");
////                    $(this)
////                        .css({
////                            transform: "translateX(0)",
////                        })
////                        .blur();
////                });
////            });
////    });


export function alterHeart(storeId) {
    Array.from(document.querySelectorAll('.clsHearts')).forEach((el) => el.classList.remove('fas'));
    document.getElementById("store_" + storeId).classList.add('fas');
}
export function displayCoupons() {
    var elts = document.getElementsByClassName('carousel-item');
    elts[0].classList.add('active');
}
export function RemoveDeleteDiv(divId) {
    var div = document.getElementById(divId);
    div.remove();
}
import TinyGesture from 'https://cdn.skypack.dev/tinygesture';
export function initSlider(target) {
    let swiped = false;
    let startOffset = 0;
    const decelerationOnOverflow = 4;
    const revealWidth = 90;
    const snapWidth = revealWidth / 1.5;

    const gesture = new TinyGesture(target);

    // swipe gestures
    gesture.on("panmove", (event) => {
        if (gesture.animationFrame) {
            return;
        }
        window.addEventListener('panmove', { passive: false })
        gesture.animationFrame = window.requestAnimationFrame(() => {
            let getX = (x) => {
                if (x < revealWidth && x < -revealWidth) {
                    return x;
                }
                //if (x < -revealWidth) {
                //    return (x + revealWidth) / decelerationOnOverflow - revealWidth;
                //}
                //if (x > revealWidth) {
                //    return (x - revealWidth) / decelerationOnOverflow + revealWidth;
                //}
            };
            const newX = getX(startOffset + gesture.touchMoveX);
            target.style.transform = "translateX(" + newX + "px)";
            if (newX >= snapWidth || newX <= -snapWidth) {
                swiped = newX < 0 ? -revealWidth : revealWidth;
            } else {
                swiped = false;
            }
            window.requestAnimationFrame(() => {
                target.style.transition = null;
            });
            gesture.animationFrame = null;
        });
    });

    gesture.on("panend", () => {
        window.cancelAnimationFrame(gesture.animationFrame);
        gesture.animationFrame = null;
        window.requestAnimationFrame(() => {
            target.style.transition = "transform .2s ease-in";
            if (!swiped) {
                startOffset = 0;
                target.style.transform = null;
            } else {
                startOffset = swiped;
                target.style.transform = "translateX(" + swiped + "px)";
            }
        });
    });

    // reset on tap
    gesture.on("doubletap", (event) => {
        // we could also use 'doubletap' here
        window.requestAnimationFrame(() => {
            target.style.transition = "transform .2s ease-in";
            swiped = false;
            startOffset = 0;
            target.style.transform = null;
        });
    });
}

export function swipemoment() {
    document.querySelectorAll(".item-swipe").forEach(initSlider);
};
export function swipemomentAddCart() {
    document.querySelectorAll(".item-swipe-addcart").forEach(initSlider);
};

export function loadFunctionForPayment() {
    /*document.addEventListener('DOMContentLoaded', function () {*/
        CollectJS.configure({
            'paymentType': 'cc',
            'callback': function (response) {
                document.getElementById("paymentTokenInfo").innerHTML =
                    '<b id="payToken">Payment Token:' + response.token + '</b> ' + /* + response.token +*/
                    '<br><b>Card:</b> ' + response.card.number +
                    '<br><b>BIN/EIN:</b> ' + response.card.bin +
                    '<br><b>Expiration:</b> ' + response.card.exp +
                    '<br><b>Hash:</b> ' + response.card.hash +
                    '<br><b>Card Type:</b> ' + response.card.type +
                    '<br><b>Check Account Name:</b> ' + response.check.name +
                    '<br><b>Check Account Number:</b> ' + response.check.account +
                    '<br><b>Check Account Hash:</b> ' + response.check.hash +
                    '<br><b>Check Routing Number:</b> ' + response.check.aba;

                var payToken = response.token;
                var token = document.getElementById("hdntoken");
                token.value = payToken;
                token.dispatchEvent(new Event('change'));
            }
        });
    //});
};
 