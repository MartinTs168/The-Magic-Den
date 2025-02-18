const cartTotalPriceField = document.querySelector('span#cartTotalPrice')
    
function updateQuantities() {
    let updateTimer;
    const timeout = 250;
    document.querySelectorAll(".quantity-input").forEach(input => {
        input.addEventListener("input", function (e) {
            clearTimeout(updateTimer);
            const errorFieldForCount = e.target.parentElement.querySelector("span[name='Count']");
            errorFieldForCount.textContent = "";  // Clear previous error message
            
            let itemTotalPriceField = e.target.closest(".item-info").querySelector('.item-total-price > span');
            let gameId = e.target.closest(".item-info").querySelector("input[name='gameId']").value;
            let newQuantity = e.target.value;
            const url = window.cartUpdateUrl;
            
            console.log(errorFieldForCount);
            
            updateTimer = setTimeout(() => {
                fetch(url, {
                    method : 'POST',
                    headers: {
                        "Content-Type": "application/json",
                        "RequestVerificationToken": getAntiForgeryToken()
                    },
                    body: JSON.stringify({GameId : gameId, Quantity: newQuantity})
                })
                .then(response => response.json())
                .then(data => {
                    if(data.success) {
                        console.log("Cart update successful")
                        updateItemPrice(itemTotalPriceField, cartTotalPriceField, gameId)
                        e.target.value = data.quantity;
                    } else {
                        errorFieldForCount.textContent = data.Quantity[0]
                    }
                })
                .catch(error => console.error(error))
            }, timeout)
            
        })
    })
    
    
}

function updateItemPrice(itemPriceField, cartTotalPriceField, id) {
    const url = window.updateItemPriceUrl + `?gameId=${id}`;
    
    fetch(url, {
        method : 'GET',
        headers: {
            "Content-Type": "application/json"
        }
    })
    .then(response => response.json())
    .then(data => {
         itemPriceField.textContent = data.itemTotalPrice;
         cartTotalPriceField.textContent = data.cartTotalPrice;
     })
    
}


function getAntiForgeryToken() {
    let tokenMeta = document.querySelector("input[name='__RequestVerificationToken']");
    return tokenMeta ? tokenMeta.value : ""; 
}

document.querySelector("select#discount_form").addEventListener("input", function(e) {
    const url = window.applyDiscountUrl;
    
    fetch(url, {
        method : 'POST',
        headers: {
            "Content-Type": "application/json",
            "RequestVerificationToken": getAntiForgeryToken()
        },
        body: JSON.stringify({Discount : Number(e.target.value)})
    }).then(response => response.json())
        .then(data => cartTotalPriceField.textContent = data.cartTotalPrice)
        .catch(error => console.error(error))
});


document.addEventListener('DOMContentLoaded', updateQuantities);